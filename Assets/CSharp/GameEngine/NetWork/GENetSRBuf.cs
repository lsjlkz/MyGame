using System.Collections.Generic;

namespace CSharp
{
    public class GENetSRBuf
    {
        
        // 缓冲区
        public GENetBuf readBuf;
        public GENetBuf writeBuf;
        
        
        public Queue<GENetBuf> bufQueue;
        public Queue<GENetBuf> bufPool;

        public GENetSRBuf()
        {
            
            this.bufQueue = new Queue<GENetBuf>();
            this.bufPool = new Queue<GENetBuf>();
            this.writeBuf = this.NewNetBuf();
            this.readBuf = this.NewNetBuf();
        }
        
        public bool WriteByte(byte[] bytes, int bufSize, int writtenBufSize)
        {
            int needBufSize = bufSize - writtenBufSize;
            int bufCanWriteSize = this.writeBuf.CanWriteSize();
            while (bufCanWriteSize < needBufSize)
            {
                this.writeBuf.WriteUnSafe(bytes, writtenBufSize, bufCanWriteSize);
                needBufSize -= bufCanWriteSize;
                writtenBufSize += bufCanWriteSize;
                // 入队
                this.bufQueue.Enqueue(this.writeBuf);
                this.writeBuf = this.NewNetBuf();
                bufCanWriteSize = this.writeBuf.CanWriteSize();
            }
            if (needBufSize == 0)
            {
                return true;
            }
            // 这个时候肯定够了
            this.writeBuf.WriteUnSafe(bytes, writtenBufSize, needBufSize);
            return true;
        }
        
        public GENetBuf NewNetBuf()
        {
            GENetBuf geNetBuf;
            if (bufPool.Count == 0)
            {
                // 池空了
                geNetBuf = new GENetBuf(GENetDefine.DEFAULT_BUF_SIZE);
            }
            else
            {
                geNetBuf = bufPool.Dequeue();
            }
            geNetBuf.Reset();
            return geNetBuf;
        }

        public void DelNetBuf(GENetBuf geNetBuf)
        {
            this.bufPool.Enqueue(geNetBuf);
        }

        public int CanReadSize()
        {
            return this.readBuf.CanReadSize();
        }

        public int CanWriteSize()
        {
            return this.writeBuf.CanWriteSize();
        }

        
    }
}