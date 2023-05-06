using System;
using System.Net;
using System.Net.Sockets;
using XLua;

namespace CSharp
{
    [LuaCallCSharp]
    public class GESocket:GESingleton<GESocket>
    {

        private Socket _socket = null;

        private bool _isConnected = false;
        private bool _isConnecting = false;


        private GENetRecv _geNetRecv = null;
        

        public GESocket()
        {
            _geNetRecv = new GENetRecv();
        }

        public Socket Socket()
        {
            return _socket;
        }
        
        public bool IsConnect()
        {
            return _isConnected;
        }

        // 连接
        public bool Connect(string host, int port)
        {
            if (this._isConnected || this._isConnecting)
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
                _socket = new Socket(af, SocketType.Stream, ProtocolType.Tcp);
                _socket.BeginConnect(ipEndPoint, new AsyncCallback(this.OnAsyncConnectMainAsync), this._socket);
                this._isConnecting = true;
            }
            catch (Exception e)
            {
                GELog.Instance().Log(e);
                return false;
            }
            return true;
        }

        private void OnAsyncConnectMainAsync(IAsyncResult iAsyncResult)
        {
            if (this._socket != iAsyncResult.AsyncState)
            {
                return;
            }

            try
            {
                // 会阻塞线程，并等待完成连接
                this._socket.EndConnect(iAsyncResult);
                this._isConnected = true;
            }
            catch (Exception e)
            {
                GELog.Instance().Log(e);
                this._isConnected = false;
            }
            finally
            {
                this._isConnecting = false;
            }
        }

        private void Disconnect()
        {
            this.DisconnectMainAsync();
        }


        private void DisconnectMainAsync()
        {
            // 主线程的中断连接
            this._isConnected = false;
            Socket s = this._socket;
            this._socket = null;
            try
            {
                // 中断连接
                s.Shutdown(SocketShutdown.Both);
                s.Close();
            }
            catch (Exception e)
            {
                GELog.Instance().Log(e);
            }
        }

        public void Update()
        {
            this.RecvMsg();
        }

        public bool RecvMsg()
        {
            if (!IsConnect())
            {
                return false;
            }
            return this._geNetRecv.RecvMsg();
        }

    }
}