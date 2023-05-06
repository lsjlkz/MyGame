using System;
using System.Net.Sockets;
using UnityEngine;

namespace CSharp
{
    public class GENetRecv:GESingleton<GENetRecv>
    {
        private GENetRecvBuf _geNetRecvBuf;

        public GENetRecv() : base()
        {
            _geNetRecvBuf = new GENetRecvBuf();
        }
        
        public GENetRecvBuf GetRecvBuf()
        {
            return _geNetRecvBuf;
        }

        public bool RecvMsg()
        {
            this.DoWriteSocketMsg();
            this.DoReadMsg();
            return true;
        }

        public bool DoWriteSocketMsg()
        {

            if (!GESocket.Instance().Socket().Poll(0, SelectMode.SelectRead))
            {
                return false;
            }
            byte[] buf = new byte[GENetDefine.DEFAULT_BUF_SIZE];
            int readBufSize = GESocket.Instance().Socket().Receive(buf, GENetDefine.DEFAULT_BUF_SIZE, SocketFlags.None);
            this._geNetRecvBuf.WriteByte(buf, readBufSize, 0);
            return true;
        }
        
        // 客户端是一块一块发送的
        public bool DoReadMsg()
        {
            
            if (this._geNetRecvBuf.MoveToNextMsg())
            {
                this.DoMsg(this._geNetRecvBuf.GetMsgBase());
            };
            return true;
        }

        public bool DoMsg(GEMsgBase geMsgBase)
        {
            byte[] bytes = geMsgBase.Bytes;
            Debug.Log(bytes.Length);
            Debug.Log(bytes.ToString());
            return true;
        }
    }
}