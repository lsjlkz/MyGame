using System.IO;
using UnityEditor;
using UnityEngine;
using XLua;

namespace CSharp
{
    public class GELua:GESingleton<GELua>
    {
        private LuaEnv _luaEnv = null;

        public bool InitLuaMainThread()
        {
            if (_luaEnv != null)
            {
                GELog.Instance().Log("repeat init lua thread");
                return false;
            }
            _luaEnv = new LuaEnv();
            _luaEnv.AddLoader(CustomMyLoader);
            return true;
        }

        public LuaEnv GetLuaMainThread()
        {
            return _luaEnv;
        }

        public bool DisposeLuaMainThread()
        {
            if (_luaEnv == null)
            {
                return true;
            }
            _luaEnv.Dispose();
            _luaEnv = null;
            return true;
        }

        public void DoString(string s)
        {
            GetLuaMainThread().DoString(s);
        }

        public void LuaTest()
        {
            Instance().DoString("require('GEInit')");
        }
        
        private byte[] CustomMyLoader(ref string fileName)
        {
            string luaPath = Application.dataPath + "/Resources/LuaCodeBin/" + fileName + ".lua.bin";
            string strLuaContent = File.ReadAllText(luaPath);
            byte[] result = System.Text.Encoding.UTF8.GetBytes(strLuaContent);
            return result;
        }
    }
}