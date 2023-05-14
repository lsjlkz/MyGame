using System;
using System.Collections.Generic;

namespace CSharp
{
    // 一个自定义实现的流类型
    // 可以返回特定位置的引用代理，方便修改特定位置
    // 为什么这个东西和GENetBuf比较像，但是又解耦了
    // 因为解耦之后可以很容易扩展使用范围，比如Cache，而非仅仅是在GENet中使用
    public class GEStream
    {
        private Queue<byte[]> _bufQueue = new Queue<byte[]>();
        private byte[] _curWriteBuf = new byte[GENetDefine.DEFAULT_BUF_SIZE];
        private int _curWriteBufFence = 0;

        public GEStreamPosRefProxy<byte> WriteUI8Ref(byte b)
        {
            if (this.CanWriteSize == 0)
            {
                this.NewBuf();
            }
            byte[] buf1 = this._curWriteBuf;
            int pos = this._curWriteBufFence;
            this.WriteBytes(new[] {b}, 1);
            return GEStreamPosRefProxy<byte>.NewRefProxy(buf1, null, pos);
        }

        public GEStreamPosRefProxy<UInt16> WriteUI16Ref(UInt16 ui16)
        {
            
        }
        
        
        // Write其他的几个
        
        private void NewBuf()
        {
            this._bufQueue.Enqueue(this._curWriteBuf);
            this._curWriteBuf = new byte[GENetDefine.DEFAULT_BUF_SIZE];
            this._curWriteBufFence = 0;
        }

        public bool WriteBytes(byte[] bytes, int size)
        {
            int writtenSize = 0;
            while (writtenSize != size)
            {
                if (this.CanWriteSize == 0)
                {
                    // 不够写了
                    this.NewBuf();
                }
                int realWriteSize = Math.Min(size, this.CanWriteSize);
                Array.Copy(bytes, writtenSize, this._curWriteBuf, this._curWriteBufFence, realWriteSize);
                writtenSize += realWriteSize;
            }

            return true;
        }

        private int CanWriteSize
        {
            get => this._curWriteBuf.Length - this._curWriteBufFence;
        }
        

        public Queue<byte[]> BufQueue
        {
            get => this._bufQueue;
        }

        public byte[] CurBuf
        {
            get => this._curWriteBuf;
        }
        
        public int CurBufFence
        {
            get => this._curWriteBufFence;

        }
        
        
    }


    // GEStream特定位置的引用代理
    public class GEStreamPosRefProxy<T>
    {
        public static readonly List<Type> LimitType = new List<Type>()
        {
            typeof(UInt16), typeof(Int16), typeof(UInt32), typeof(Int32),typeof(Byte)
        };
        // 这个value修改可以会横跨两个byte[]
        // 比如byte[4]的前2个在buf1，后2个在buf2
        public byte[] _buf1; //对应的stream
        public byte[] _buf2;
        
        public int _pos;

        public static GEStreamPosRefProxy<T> NewRefProxy(byte[] buf1, byte[] buf2, int pos)
        {
            if (!LimitType.Contains(typeof(T)))
            {
                GELog.Instance().Log($"error NewRefProxy error type{typeof(T).FullName}");
                return null;
            }
            return new GEStreamPosRefProxy<T>(buf1, buf2, pos);
        }

        private GEStreamPosRefProxy(byte[] buf1, byte[] buf2, int pos)
        {
            _buf1 = buf1;
            _buf2 = buf2;
            _pos = pos;
        }

        public bool WriteValue(UInt16 ui16)
        {
            if (typeof(T) != ui16.GetType())
            {
                return false;
            }
            Array.Copy(BitConverter.GetBytes(ui16), 0, _buf1, _pos, sizeof(UInt16));
            return true;
        }
        
        public bool WriteValue(Int16 i16)
        {
            if (typeof(T) != i16.GetType())
            {
                return false;
            }
            Array.Copy(BitConverter.GetBytes(i16), 0, _buf1, _pos, sizeof(Int16));
            return true;
        }
        
        public bool WriteValue(UInt32 ui32)
        {
            if (typeof(T) != ui32.GetType())
            {
                return false;
            }
            Array.Copy(BitConverter.GetBytes(ui32), 0, _buf1, _pos, sizeof(UInt32));
            return true;
        }
        
        public bool WriteValue(Int32 i32)
        {
            if (typeof(T) != i32.GetType())
            {
                return false;
            }
            Array.Copy(BitConverter.GetBytes(i32), 0, _buf1, _pos, sizeof(Int32));
            return true;
        }

    }
}