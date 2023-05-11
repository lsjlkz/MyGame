using System;
using System.IO;

namespace CSharp
{
    public class GEPackMessage:GESingleton<GEPackMessage>
    {
        private bool isOK;
        // 当前的流对象
        private MemoryStream curBufPack;
        private int curBufPackFence = 0;

        public bool PackMsgType(UInt16 t)
        {
            // TODO 
            curBufPack.Write(BitConverter.GetBytes(t), curBufPackFence, sizeof(UInt16));
            return true;
        }
    }
}