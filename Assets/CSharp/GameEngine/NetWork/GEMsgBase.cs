using System;

namespace CSharp
{
    public class GEMsgBase
    {
        
        private int _msgType = 0;
        private int _msgSize = 0;
        private int _writtenBufSize = 0;
        private byte[] _buf = null;
        private int _needReadSize = 0;

        public GEMsgBase(Int16 msgType, UInt16 msgSize)
        {
            // 为什么这里-4，因为头占了4个字节
            _msgType = msgType;
            _msgSize = msgSize - 4;
            _buf = new byte[msgSize - 4];
            _writtenBufSize = 0;
            _needReadSize = msgSize - 4;
        }

        public bool WriteBytes(GENetBuf geNetBuf)
        {
            _writtenBufSize += geNetBuf.ReadBytes(_buf, _writtenBufSize, _needReadSize);
            _needReadSize = _msgSize - _writtenBufSize;
            return _needReadSize != 0;
        }

        public int GetNeedReadSize()
        {
            return this._needReadSize;
        }

        public byte[] Bytes
        {
            get => this._buf;
        }
    }
}