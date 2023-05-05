using System;
using System.Net;
using System.Net.Sockets;
using CSharp.Log;
using XLua;

namespace CSharp
{
    [LuaCallCSharp]
    public class GESocket
    {
        private Socket socket = null;

        private int sendBufSize = 0;
        private int recvBufSize = 0;
        private bool isConnected = false;
        private bool isConnecting = false;
        

        public GESocket(int _sendBufSize, int _recvBufSize)
        {
            sendBufSize = _sendBufSize;
            recvBufSize = _recvBufSize;
        }

        // 连接
        private bool Connect(string host, int port)
        {
            if (this.isConnected || this.isConnecting)
            {
                return false;
            }

            try
            {
                AddressFamily af = GENetCommon.GetNetType(host);
                if (af == AddressFamily.Unknown)
                {
                    // 未知ip
                    return false;
                }

                IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse(host), port);
                // 连接
                socket = new Socket(af, SocketType.Stream, ProtocolType.Tcp);
                socket.BeginConnect(ipEndPoint, new AsyncCallback(this.OnAsyncConnect_ma), this.socket);
                this.isConnecting = true;
            }
            catch (Exception e)
            {
                GELog.instance().Log(e);
                return false;
            }
            return true;
        }

        private void OnAsyncConnect_ma(IAsyncResult iAsyncResult)
        {
            if (this.socket != iAsyncResult.AsyncState)
            {
                return;
            }

            try
            {
                // 会阻塞线程，并等待完成连接
                this.socket.EndConnect(iAsyncResult);
                this.isConnected = true;
            }
            catch (Exception e)
            {
                GELog.instance().Log(e);
                this.isConnected = false;
            }
            finally
            {
                this.isConnecting = false;
            }
        }

        private void Disconnect()
        {
            this.Disconnect_ma();
        }


        private void Disconnect_ma()
        {
            // 主线程的中断连接
            this.isConnected = false;
            Socket s = this.socket;
            this.socket = null;
            try
            {
                // 中断连接
                s.Shutdown(SocketShutdown.Both);
                s.Close();
            }
            catch (Exception e)
            {
                GELog.instance().Log(e);
            }
        }
    }
}