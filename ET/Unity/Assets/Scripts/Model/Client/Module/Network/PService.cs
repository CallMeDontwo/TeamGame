using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace ET
{
    [EnableClass]
    public sealed class PService : AService
    {
        public readonly ConcurrentQueue<TArgs> Queue = new();
        private readonly Dictionary<long, PChannel> idChannels = new();
        private readonly Dictionary<long, ETTask> tasks = new();

        public PService(AddressFamily addressFamily, ServiceType serviceType)
        {
            this.Id = IdGenerater.Instance.GenerateId();
            this.ServiceType = serviceType;
        }

        public override void Create(long id, IPEndPoint ipEndPoint)
        {
            if (!this.idChannels.ContainsKey(id))
            {
                this.idChannels.Add(id, new PChannel(id, ipEndPoint, this, null));
            }
        }

        public async ETTask CreateAsync(long id, IPEndPoint ipEndPoint)
        {
            if (!this.idChannels.ContainsKey(id))
            {
                ETTask task = ETTask.Create(true);
                this.idChannels.Add(id, new PChannel(id, ipEndPoint, this, this.ConnectCallback));
                this.tasks.Add(id, task);
                await task;
            }
        }

        private void ConnectCallback(long id, int error)
        {
            this.tasks.Remove(id, out ETTask task);
            if (error == 0)
                task.SetResult();
            else
                task.SetException(new RpcException(error, "Connect Error"));
        }

        public override void Remove(long id, int error = 0)
        {
            if (this.idChannels.Remove(id, out PChannel channel))
            {
                channel.Error = error;
                channel.Dispose();
            }
        }

        public PChannel Get(long id)
        {
            this.idChannels.TryGetValue(id, out PChannel channel);
            return channel;
        }

        public override void Send(long channelId, MemoryBuffer memoryBuffer)
        {
            try
            {
                PChannel aChannel = this.Get(channelId);
                if (aChannel == null)
                {
                    this.ErrorCallback(channelId, ErrorCore.ERR_SendMessageNotFoundTChannel);
                    return;
                }

                aChannel.Send(memoryBuffer);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public override void Update()
        {
            while (this.Queue.TryDequeue(out TArgs result))
            {
                SocketAsyncEventArgs e = result.SocketAsyncEventArgs;
                if (e == null)
                {
                    switch (result.Op)
                    {
                        case TcpOp.Connect:
                            {
                                this.Get(result.ChannelId)?.ConnectAsync();
                                break;
                            }
                        case TcpOp.StartSend:
                            {
                                this.Get(result.ChannelId)?.StartSend();
                                break;
                            }
                        case TcpOp.StartRecv:
                            {
                                this.Get(result.ChannelId)?.StartRecv();
                                break;
                            }
                    }
                }
                else
                {
                    switch (e.LastOperation)
                    {
                        case SocketAsyncOperation.Connect:
                            {
                                this.Get(result.ChannelId)?.OnConnectComplete(e);
                                break;
                            }
                        case SocketAsyncOperation.Disconnect:
                            {
                                this.Get(result.ChannelId)?.OnDisconnectComplete(e);
                                break;
                            }
                        case SocketAsyncOperation.Send:
                            {
                                this.Get(result.ChannelId)?.OnSendComplete(e);
                                break;
                            }
                        case SocketAsyncOperation.Receive:
                            {
                                this.Get(result.ChannelId)?.OnRecvComplete(e);
                                break;
                            }
                        default:
                            throw new ArgumentOutOfRangeException($"{e.LastOperation}");
                    }
                }
            }
        }

        public override void Dispose()
        {
            this.Id = 0;
            this.idChannels.Values.Foreach(channel => channel.Dispose());
            this.idChannels.Clear();
            base.Dispose();
        }

        public override bool IsDisposed()
        {
            return this.Id == 0;
        }
    }
}