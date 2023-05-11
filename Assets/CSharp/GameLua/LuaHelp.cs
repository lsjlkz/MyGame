using System.IO;
using UnityEngine;
using XLua;

namespace CSharp
{
    [LuaCallCSharp]
    public class LuaHelp
    {

        public static void LoadPackageAllScript(string package)
        {
            string path = PathHelp.GetLuaCodePath() + "\\" + package;
            DoPathAllScript(path);
        }

        public static string GetLuaScriptPath(string path)
        {
            return PathHelp.GetLuaCodePath() + "\\" + path;
        }

        public static void DoPathAllScript(string path)
        {
            foreach (string dir in Directory.GetDirectories(path))
            {
                DoPathAllScript(dir);
            }

            foreach (string file in Directory.GetFiles(path))
            {
                if (!file.EndsWith(PathHelp.LuaCodeBinEnd))
                {
                    continue;
                }
                GELua.Instance().DoFile(file);
            }
        }

        public static GameObject LoadPrefab(string path)
        {
            GameObject prefab = null;
            if (!ResourceMgr.Instance().LoadPrefab(path, ref prefab))
            {
                return null;
            }

            GameObject instantiatedPrefab = GameObject.Instantiate(prefab);
            return instantiatedPrefab;
        }
    }
}