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

        private AsyncCallback _sendCallback;


        private GENetRecv _geNetRecv = null;
        private GENetSend _geNetSend = null;

        public GESocket()
        {
            _geNetRecv = new GENetRecv();
            _geNetSend = new GENetSend();

            _sendCallback = new AsyncCallback(this.OnAsyncSend_a);
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
            if (!IsConnect())
            {
                return;
            }
            this.RecvMsg();
        }

        public void RecvMsg()
        {
            this._geNetRecv.RecvMsg();
        }

        public bool WriteMsg(byte[] bytes, int length)
        {
            if (!IsConnect())
            {
                return false;
            }
            return this._geNetSend.WriteBytes(bytes, length);
        }

        public void SendMsg()
        {
            
            GENetBuf geNetBuf = this._geNetSend.HoldOneBlock();
            if (geNetBuf == null)
            {
                // 没有发送的
                return;
            }
            // TODO 4k对齐
            Socket().BeginSend(geNetBuf.Buf, geNetBuf.ReadSize, geNetBuf.CanReadSize(), SocketFlags.None, this._sendCallback,
                Socket());
        }

        private void OnAsyncSend_a(IAsyncResult ar)
        {
            
            if (Socket() != ar.AsyncState)
            {
                return;
            }

            int sendSize = 0;
            sendSize = Socket().EndSend(ar);
            if (sendSize == 0)
            {
                this._geNetSend.ReleaseHold();
                Disconnect();
                return;
            }

            if (!this._geNetSend.SendingNetBuf.IncReadSize(sendSize))
            {
                this._geNetSend.ReleaseHold();
                return;
            }

            if (this._geNetSend.FinishSending)
            {
                this._geNetSend.ReleaseHold();
                return;
            }

            SendMsg();

        }
    }
}