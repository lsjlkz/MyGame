namespace CSharp
{
    public class GENetSend
    {
        private GENetSendBuf _geNetSendBuf;

        private GENetBuf _sendNetBuf = null;

        public GENetSend()
        {
            _geNetSendBuf = new GENetSendBuf();
        }

        public GENetBuf SendingNetBuf
        {
            get => this._sendNetBuf;
        }

        public bool FinishSending
        {
            get => this._sendNetBuf.CanReadSize() == 0;
        }

        public GENetBuf HoldOneBlock()
        {

            bool ret = _geNetSendBuf.HoldBlock();
            if (ret == false)
            {
                return null;
            }

            _sendNetBuf = _geNetSendBuf.GetReadBuf();

            return _sendNetBuf;

        }

        public void ReleaseHold()
        {
            _sendNetBuf = null;
            this._geNetSendBuf.ReleaseHold();
        }

        public bool WriteBytes(byte[] bytes, int length)
        {
            return this._geNetSendBuf.WriteByte(bytes, length, 0);
        }
    }
}