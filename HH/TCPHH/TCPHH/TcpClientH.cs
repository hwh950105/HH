using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPHH
{
    public class TcpClientH
    {


        public delegate void OnConnectDelegate(object sender);

        public delegate void OnDisconnectDelegate(object sender);

        public delegate void OnDataAvailableDelegate(object sender, string data, byte[] _data, string byteString);

        public delegate void OnErrorDelegate(object sender, string errorMessage);

        public class StateObject
        {
            public const int BufferSize = 1024;

            public Socket workSocket;

            public byte[] buffer = new byte[1024];

            public StringBuilder sb = new StringBuilder();
        }

        private string host = "192.168.20.1";

        private int port = 8080;

        private TcpClient client;

        public readonly int InstanceID;

        private static int NextInstanceID;

        private static long ClassInstanceCount;

        public static long InstanceCount => ClassInstanceCount;

        public int Port
        {
            get
            {
                return port;
            }
            set
            {
                port = value;
            }
        }

        public string Host
        {
            get
            {
                return host;
            }
            set
            {
                host = value;
            }
        }

        public bool Connected
        {
            get
            {
                if (client != null)
                {
                    return client.Client.Connected;
                }

                return false;
            }
        }

        public event OnConnectDelegate OnConnect;

        public event OnDisconnectDelegate OnDisconnect;

        public event OnDataAvailableDelegate OnDataAvailable;

        public event OnErrorDelegate OnError;

        public TcpClientH()
        {
            InstanceID = NextInstanceID++;
            ClassInstanceCount++;
        }

        ~TcpClientH()
        {
            ClassInstanceCount--;
        }

        public void Connect()
        {
            try
            {
                client = new TcpClient(host, port);
                StateObject stateObject = new StateObject();
                stateObject.workSocket = client.Client;
                client.Client.BeginReceive(stateObject.buffer, 0, 1024, SocketFlags.None, ReadCallback, stateObject);
                if (this.OnConnect != null)
                {
                    this.OnConnect(this);
                }
            }
            catch (Exception ex)
            {
                if (this.OnError != null)
                {
                    this.OnError(this, ex.Message);
                }
            }
        }

        public void Disconnect()
        {
            if (!client.Client.Connected)
            {
                return;
            }

            try
            {
                client.Client.Close();
            }
            catch (Exception ex)
            {
                if (this.OnError != null)
                {
                    this.OnError(this, ex.Message);
                }

                return;
            }

            if (client.Connected && this.OnError != null)
            {
                this.OnError(this, "Client still connected");
            }
        }

        private void ReadCallback(IAsyncResult ar)
        {
            _ = string.Empty;
            StateObject stateObject = (StateObject)ar.AsyncState;
            Socket workSocket = stateObject.workSocket;
            int num = 0;
            try
            {
                num = workSocket.EndReceive(ar);
            }
            catch (SocketException)
            {
                if (this.OnDisconnect != null)
                {
                    this.OnDisconnect(this);
                }

                return;
            }
            catch (ObjectDisposedException)
            {
                if (this.OnDisconnect != null)
                {
                    this.OnDisconnect(this);
                }

                return;
            }
            catch (Exception ex3)
            {
                if (this.OnError != null)
                {
                    this.OnError(this, ex3.Message);
                }

                return;
            }

            if (num > 0)
            {
                string @string = Encoding.ASCII.GetString(stateObject.buffer, 0, num);
                byte[] array = new byte[num];
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = stateObject.buffer[i];
                }
                string byteString = BitConverter.ToString(array).Replace("-", " ");

                if (this.OnDataAvailable != null)
                {
                    this.OnDataAvailable(this, @string, array, byteString);
                }

                try
                {
                    workSocket.BeginReceive(stateObject.buffer, 0, 1024, SocketFlags.None, ReadCallback, stateObject);
                }
                catch (Exception ex4)
                {
                    if (this.OnError != null)
                    {
                        this.OnError(this, ex4.Message);
                    }
                }
            }
            else if (this.OnDisconnect != null)
            {
                this.OnDisconnect(this);
            }
        }

        public void Send(string data)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(data);
            client.Client.BeginSend(bytes, 0, bytes.Length, SocketFlags.None, SendCallback, client.Client);
        }

        public void SendBytes(byte[] data)
        {
            client.Client.BeginSend(data, 0, data.Length, SocketFlags.None, SendCallback, client.Client);
        }

        public void SendByteString(string data)
        {
            data = data.Replace(" ", string.Empty);

            if (data.Length % 2 != 0)
            {
                throw new ArgumentException("Data length must be even.");
            }

            byte[] bytes = new byte[data.Length / 2];

            for (int i = 0; i < bytes.Length; i++)
            {
                string hexPair = data.Substring(i * 2, 2);
                bytes[i] = Convert.ToByte(hexPair, 16);
            }

            client.Client.BeginSend(bytes, 0, bytes.Length, SocketFlags.None, SendCallback, client.Client);
        }


        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket socket = (Socket)ar.AsyncState;
                socket.EndSend(ar);
            }
            catch (Exception ex)
            {
                if (this.OnError != null)
                {
                    this.OnError(this, ex.Message);
                }
            }
        }
    }
}
