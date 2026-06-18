using System.IO.Ports;
using UnityEngine;

namespace ET
{
    [EnableClass]
    public sealed class SerialController : MonoBehaviour
    {
        public string portName;
        public int baudRate = 9600;
        public SerialPort SerialPort { get; private set; }

        private readonly byte[] buffer = new byte[1];

        private void Awake()
        {
            this.SerialPort = new SerialPort(this.portName, this.baudRate);
            this.SerialPort.Open();
        }

        public void Write(string text)
        {
            if (this.SerialPort.IsOpen)
                this.SerialPort.Write(text);
        }

        public void WriteLine(string text)
        {
            if (this.SerialPort.IsOpen)
                this.SerialPort.WriteLine(text);
        }

        public void WriteByte(int num)
        {
            this.buffer[0] = (byte)num;
            this.Write(this.buffer);
        }

        public void WriteChar(char ch)
        {
            this.buffer[0] = (byte)ch;
            this.Write(this.buffer);
        }

        public void Write(byte[] buffer)
        {
            this.Write(buffer, 0, this.buffer.Length);
        }

        public void Write(byte[] buffer, int offset, int count)
        {
            if (this.SerialPort.IsOpen)
                this.SerialPort.Write(buffer, offset, count);
        }

        public void Write(char[] buffer)
        {
            this.Write(buffer, 0, buffer.Length);
        }

        public void Write(char[] buffer, int offset, int count)
        {
            if (this.SerialPort.IsOpen)
                this.SerialPort.Write(buffer, offset, count);
        }
    }
}