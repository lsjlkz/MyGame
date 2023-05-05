using System.IO;
using CSharp.Log;
using UnityEditor;
using UnityEngine;
using XLua;

namespace CSharp.Game
{
    public class GELua
    {
        private static GELua _instance = null;
        private LuaEnv _luaEnv = null;

        public static GELua instance()
        {
            if (_instance == null)
            {
                _instance = new GELua();
            }
            return _instance;
        }
        public bool initLuaThread()
        {
            if (_luaEnv != null)
            {
                GELog.instance().Log("repeat init lua thread");
                return false;
            }
            _luaEnv = new LuaEnv();
            _luaEnv.AddLoader(CustomMyLoader);
            return true;
        }

        public LuaEnv luaThread()
        {
            return _luaEnv;
        }

        public bool disposeLuaThread()
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
            luaThread().DoString(s);
        }

        public void luaTest()
        {
            instance().DoString("require('GEInit')");
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