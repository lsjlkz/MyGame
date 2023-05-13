using System;
using System.IO;

namespace CSharp
{
    public class GEPackMessage:GESingleton<GEPackMessage>
    {
        private bool isOK;

        public bool PackMsgType(UInt16 t)
        {
            return true;
        }
        
    }
}