using System;

namespace CSharp
{
    public class GENetBuf
    {
        private byte[] buf = null;
        private int readSize = 0;
        private int writeSize = 0;
        private int bufSize = 0;

        public GENetBuf(int _bufSize)
        {
            this.bufSize = _bufSize;
            buf = new byte[this.bufSize];
        }

        // 重置
        public void Reset(int _readSize=0, int _writeSize = 0)
        {
            this.readSize = _readSize;
            this.writeSize = _writeSize;
        }

        public void WriteUnSafe(byte[] _buf, int offset, int length)
        {
            Array.Copy(_buf, offset, this.buf, this.writeSize, length);
            this.writeSize += length;
        }

        public int CanReadSize()
        {
            return this.writeSize - this.readSize;
        }

        public int CanWriteSize()
        {
            return this.bufSize - this.writeSize;
        }

        public UInt32 ReadUI8()
        {
            if (this.CanReadSize() < 1)
            {
                GELog.Instance().Log("error read ui8 too short");
                return 0;
            }
            byte b = this.buf[this.readSize];
            this.readSize += 1;
            return Convert.ToUInt32(b);
        }

        public Int32 ReadI8()
        {
            if (this.CanReadSize() < 1)
            {
                GELog.Instance().Log("error read i8 too short");
                return 0;
            }
            byte b = this.buf[this.readSize];
            this.readSize += 1;
            return Convert.ToInt32(b);
        }
        
        public UInt16 ReadUI16()
        {
            if (this.CanReadSize() < sizeof(UInt16))
            {
                GELog.Instance().Log("error read ui16 too short");
                return 0;
            }
            UInt16 ui16 = BitConverter.ToUInt16(this.buf, this.readSize);
            this.readSize += sizeof(UInt16);
            return ui16;
        }
        
        public Int16 ReadI16()
        {
            if (this.CanReadSize() < sizeof(Int16))
            {
                GELog.Instance().Log("error read i16 too short");
                return 0;
            }
            Int16 i16 = BitConverter.ToInt16(this.buf, this.readSize);
            this.readSize += sizeof(Int16);
            return i16;
        }
        
        public UInt32 ReadUI32()
        {
            if (this.CanReadSize() < sizeof(UInt32))
            {
                GELog.Instance().Log("error read ui32 too short");
                return 0;
            }
            UInt32 ui32 = BitConverter.ToUInt32(this.buf, this.readSize);
            this.readSize += sizeof(UInt32);
            return ui32;
        }

        public Int32 ReadI32()
        {
            if (this.CanReadSize() < sizeof(Int32))
            {
                GELog.Instance().Log("error read i32 too short");
                return 0;
            }
            Int32 i32 = BitConverter.ToInt32(this.buf, this.readSize);
            this.readSize += sizeof(Int32);
            return i32;
        }

        public int ReadBytes(byte[] bytes, int offset, int size)
        {
            int copySize = Math.Min(this.CanReadSize(), size);
            Array.Copy(this.buf, this.readSize, bytes, offset, copySize);
            this.readSize += copySize;
            return copySize;
        }
    }
}