using System.Collections.Generic;

namespace CSharp
{
    public class GENetSendBuf:GENetSRBuf
    {

        public bool isHold;

        public GENetSendBuf():base()
        {
            isHold = false;
        }

        public bool HoldBlock()
        {
            if (this.isHold)
            {
                // 已经hold住了
                return false;
            }
            if (this.bufQueue.Count != 0)
            {
                // 发送队列里面有一个
                this.readBuf = this.bufQueue.Dequeue();
            }
            else
            {
                // 队列里面的都发送了
                // 如果write有内容的话，那就把write的给发送了
                // 先交换
                if (this.writeBuf.CanReadSize() == 0)
                {
                    return false;
                }
                (this.writeBuf, this.readBuf) = (this.readBuf, this.writeBuf);
                this.writeBuf.Reset();
            }
            this.isHold = true;
            return true;
        }

        public void WriteStream(GEStream geStream)
        {
            foreach (byte[] buf in geStream.BufQueue)
            {
                this.WriteByte(buf, buf.Length, 0);
            }

            this.WriteByte(geStream.CurBuf, geStream.CurBufFence, 0);
        }
    }
}