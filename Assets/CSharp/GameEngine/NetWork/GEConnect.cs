using XLua;

namespace CSharp
{
    [LuaCallCSharp]
    public class GEConnect
    {
        public static bool Connect(string host, int port)
        {
            return GESocket.Instance().Connect(host, port);
        }
    }

}