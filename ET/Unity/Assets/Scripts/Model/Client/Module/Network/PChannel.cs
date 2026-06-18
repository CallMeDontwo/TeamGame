using System;
using System.Net;
using System.Net.Sockets;

namespace ET
{
    [EnableClass]
    public sealed class PChannel : AChannel
    {
        private readonly PService Service;
        private readonly PPacketParser parser;

        private Socket socket;
        private SocketAsyncEventArgs innArgs = new();
        private SocketAsyncEventArgs outArgs = new();

        private readonly CircularBuffer recvBuffer = new();
        private readonly CircularBuffer sendBuffer = new();

        private bool isSending;
        //private bool isConnected;

        private Action<long, int> onConnect;

        public PChannel(long id, IPEndPoint ipEndPoint, PService service, Action<long, int> onConnect)
        {
            this.Id = id;
            this.Service = service;
            this.onConnect = onConnect;
            this.ChannelType = ChannelType.Connect;
            this.socket = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            this.socket.NoDelay = true;
            this.parser = new PPacketParser(this.Service, this.recvBuffer);

            this.innArgs.Completed += this.OnComplete;
            this.outArgs.Completed += this.OnComplete;

            //this.isConnected = false;
            this.isSending = false;
            this.RemoteAddress = ipEndPoint;

            this.Service.Queue.Enqueue(new TArgs() { Op = TcpOp.Connect, ChannelId = this.Id });
        }

        private void OnComplete(object sender, SocketAsyncEventArgs e)
        {
            this.Service.Queue.Enqueue(new TArgs() { ChannelId = this.Id, SocketAsyncEventArgs = e });
        }

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }

            Log.Info($"channel dispose: {this.Id} {this.RemoteAddress} {this.Error}");

            long id = this.Id;
            this.Id = 0;
            this.Service.Remove(id);
            this.socket.Close();
            this.innArgs.Dispose();
            this.outArgs.Dispose();
            this.socket.Dispose();
            this.innArgs = null;
            this.outArgs = null;
            this.socket = null;
        }

        public void Send(MemoryBuffer stream)
        {
            if (this.IsDisposed)
            {
                throw new Exception("TChannel已经被Dispose, 不能发送消息");
            }

            this.sendBuffer.Write(stream.GetBuffer(), (int)stream.Position, (int)(stream.Length - stream.Position));
            if (!this.isSending)
            {
                this.Service.Queue.Enqueue(new TArgs() { Op = TcpOp.StartSend, ChannelId = this.Id });
            }

            this.Service.Recycle(stream);
        }

        public void ConnectAsync()
        {
            this.outArgs.RemoteEndPoint = this.RemoteAddress;
            if (this.socket.ConnectAsync(this.outArgs))
            {
                return;
            }
            this.OnConnectComplete(this.outArgs);
        }

        public void OnConnectComplete(SocketAsyncEventArgs e)
        {
            if (this.socket == null)
            {
                return;
            }

            if (e.SocketError != SocketError.Success)
            {
                var id = this.Id;
                this.OnError((int)e.SocketError);
                this.onConnect?.Invoke(id, (int)e.SocketError);
                this.onConnect = null;
                return;
            }

            e.RemoteEndPoint = null;
            //this.isConnected = true;
            this.onConnect?.Invoke(this.Id, 0);
            this.onConnect = null;

            this.Service.Queue.Enqueue(new TArgs() { Op = TcpOp.StartSend, ChannelId = this.Id });
            this.Service.Queue.Enqueue(new TArgs() { Op = TcpOp.StartRecv, ChannelId = this.Id });
        }

        public void OnDisconnectComplete(SocketAsyncEventArgs e)
        {
            this.OnError((int)e.SocketError);
        }

        public void StartRecv()
        {
            while (true)
            {
                try
                {
                    if (this.socket == null)
                    {
                        return;
                    }

                    int size = this.recvBuffer.ChunkSize - this.recvBuffer.LastIndex;
                    this.innArgs.SetBuffer(this.recvBuffer.Last, this.recvBuffer.LastIndex, size);
                }
                catch (Exception e)
                {
                    Log.Error($"tchannel error: {this.Id}\n{e}");
                    this.OnError(ErrorCore.ERR_TChannelRecvError);
                    return;
                }

                if (this.socket.ReceiveAsync(this.innArgs))
                {
                    return;
                }
                this.HandleRecv(this.innArgs);
            }
        }

        public void OnRecvComplete(SocketAsyncEventArgs o)
        {
            this.HandleRecv(o);

            if (this.socket == null)
            {
                return;
            }

            this.Service.Queue.Enqueue(new TArgs() { Op = TcpOp.StartRecv, ChannelId = this.Id });
        }

        private void HandleRecv(SocketAsyncEventArgs e)
        {
            if (this.socket == null)
            {
                return;
            }
            if (e.SocketError != SocketError.Success)
            {
                this.OnError((int)e.SocketError);
                return;
            }

            if (e.BytesTransferred == 0)
            {
                this.OnError(ErrorCore.ERR_PeerDisconnect);
                return;
            }

            this.recvBuffer.LastIndex += e.BytesTransferred;
            if (this.recvBuffer.LastIndex == this.recvBuffer.ChunkSize)
            {
                this.recvBuffer.AddLast();
                this.recvBuffer.LastIndex = 0;
            }

            // 收到消息回调
            while (true)
            {
                // 这里循环解析消息执行，有可能，执行消息的过程中断开了session
                if (this.socket == null)
                {
                    return;
                }
                try
                {
                    if (this.recvBuffer.Length == 0)
                    {
                        break;
                    }
                    bool ret = this.parser.Parse(out MemoryBuffer memoryBuffer);
                    if (!ret)
                    {
                        break;
                    }

                    this.OnRead(memoryBuffer);
                }
                catch (Exception ee)
                {
                    Log.Error($"ip: {this.RemoteAddress} {ee}");
                    this.OnError(ErrorCore.ERR_SocketError);
                    return;
                }
            }
        }

        public void StartSend()
        {
            if (this.isSending)
            {
                return;
            }

            while (true)
            {
                try
                {
                    if (this.socket == null)
                    {
                        this.isSending = false;
                        return;
                    }

                    // 没有数据需要发送
                    if (this.sendBuffer.Length == 0)
                    {
                        this.isSending = false;
                        return;
                    }

                    this.isSending = true;

                    int sendSize = this.sendBuffer.ChunkSize - this.sendBuffer.FirstIndex;
                    if (sendSize > this.sendBuffer.Length)
                    {
                        sendSize = (int)this.sendBuffer.Length;
                    }
                    this.outArgs.SetBuffer(this.sendBuffer.First, this.sendBuffer.FirstIndex, sendSize);

                    if (this.socket.SendAsync(this.outArgs))
                    {
                        return;
                    }

                    this.HandleSend(this.outArgs);
                }
                catch (Exception e)
                {
                    throw new Exception($"socket set buffer error: {this.sendBuffer.First.Length}, {this.sendBuffer.FirstIndex}", e);
                }
            }
        }

        public void OnSendComplete(SocketAsyncEventArgs o)
        {
            this.HandleSend(o);

            this.isSending = false;

            this.Service.Queue.Enqueue(new TArgs() { Op = TcpOp.StartSend, ChannelId = this.Id });
        }

        private void HandleSend(SocketAsyncEventArgs e)
        {
            if (this.socket == null)
            {
                return;
            }

            if (e.SocketError != SocketError.Success)
            {
                this.OnError((int)e.SocketError);
                return;
            }

            if (e.BytesTransferred == 0)
            {
                this.OnError(ErrorCore.ERR_PeerDisconnect);
                return;
            }

            this.sendBuffer.FirstIndex += e.BytesTransferred;
            if (this.sendBuffer.FirstIndex == this.sendBuffer.ChunkSize)
            {
                this.sendBuffer.FirstIndex = 0;
                this.sendBuffer.RemoveFirst();
            }
        }

        private void OnRead(MemoryBuffer memoryStream)
        {
            try
            {
                this.Service.ReadCallback(this.Id, memoryStream);
            }
            catch (Exception e)
            {
                Log.Error(e);
                this.OnError(ErrorCore.ERR_PacketParserError);
            }
        }

        private void OnError(int error)
        {
            Log.Info($"TChannel OnError: {error} {this.RemoteAddress}");

            long channelId = this.Id;

            this.Service.Remove(channelId, error);

            this.Service.ErrorCallback(channelId, error);
        }
    }
}