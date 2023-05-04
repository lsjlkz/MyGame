using System.Net;
using System.Net.Sockets;

namespace CSharp
{
    public class GCNetCommon
    {
        public static AddressFamily GetNetType(string host)
        {
            // 连接DNS看看这个host对应哪个ip
            IPAddress[] ips = Dns.GetHostAddresses(host);
            for (int i = 0; i < ips.Length; i++)
            {
                if (ips[i].AddressFamily == AddressFamily.InterNetwork || ips[i].AddressFamily == AddressFamily.InterNetworkV6)
                {
                    return ips[i].AddressFamily;
                }
            }
            return AddressFamily.Unknown;
        }
    }
}