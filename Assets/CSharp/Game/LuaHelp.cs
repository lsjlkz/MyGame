using System.IO;
using XLua;

namespace CSharp
{
    [LuaCallCSharp]
    public class LuaHelp
    {
        public static string[] GetDirectories(string path)
        {
            return Directory.GetDirectories(path);
        }

        public static string[] GetFiles(string path)
        {
            return Directory.GetFiles(path);
        }
    }
}