using System;

namespace CSharp
{
    public class GENetRecvBuf:GENetSRBuf
    {
        
        private bool _isReading = false;
        private GEMsgBase _geMsgBase = null;

        public GENetRecvBuf():base()
        {
            
        }

        public bool MoveToNextMsg()
        {
            if (this._isReading)
            {
                // 还在读
                return this.RecoverReadMsg();
            }
            if (!this.CanReadBuf())
            {
                // 空了，不用读
                return false;
            }
            this._isReading = true;
            Int16 msgType = this.readBuf.ReadI16();
            UInt16 msgSize = this.readBuf.ReadUI16();
            this._geMsgBase = new GEMsgBase(msgType, msgSize);
            return this.DoReadMsg();
        }


        public bool RecoverReadMsg()
        {
            if (this._geMsgBase == null)
            {
                // TODO 居然是null
                this._isReading = false;
                return true;
            }
            return this.DoReadMsg();
        }

        public bool DoReadMsg()
        {
            while (this._geMsgBase.WriteBytes(this.readBuf))
            {
                if (!this.CanReadBuf())
                {
                    return false;
                }
            }
            this._isReading = false;
            return true;
        }

        public bool CanReadBuf()
        {
            if (this.CanReadSize() > 0)
            {
                // 至少还可以读到一个字节
                return true;
            }
            if (this.bufQueue.Count != 0)
            {
                this.DelNetBuf(this.readBuf);
                this.readBuf = this.bufQueue.Dequeue();
                return true;
            }
            // 队列也没有了
            if (this.writeBuf.CanReadSize() >= 4)
            {
                // 写缓冲区还可以读
                (this.readBuf, this.writeBuf) = (this.writeBuf, this.readBuf);
                this.writeBuf.Reset();
                return true;
            }
            return false;
        }

        public GEMsgBase GetMsgBase()
        {
            return this._geMsgBase;
        }
        
    }

}