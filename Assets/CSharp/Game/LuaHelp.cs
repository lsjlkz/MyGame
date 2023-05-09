using System.IO;
using UnityEngine.WSA;
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

        public static void LoadPackageAllScript(string package)
        {
            string path = UnityEngine.Application.dataPath + "\\Resources\\LuaCodeBin\\" + package;
        }
    }
}