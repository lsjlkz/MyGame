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

        public void Tick()
        {
            GetLuaMainThread().Tick();
        }

        public object[] DoString(string s)
        {
            return GetLuaMainThread().DoString(s);
        }

        public LuaTable LoadString(string s)
        {
            return GetLuaMainThread().LoadString<LuaTable>(s);
        }
        
        public LuaTable LoadTable(string luaPath)
        {
            string path = LuaHelp.GetLuaScriptPath(luaPath);
            string luaText = File.ReadAllText(path);
            return GetLuaMainThread().LoadString<LuaTable>(luaText);
        }

        public void DoFile(string absPath)
        {
            string luaText = File.ReadAllText(absPath);
            object[] ret = DoString(luaText);
            if (ret.Length == 0)
            {
                return;
            }
            LuaTable luaTable = (LuaTable) ret[0];
            LuaFunction init = luaTable.Get<LuaFunction>("init");
            object isInit = luaTable.Get<object>("is_init");
            if (isInit != null || init == null)
            {
                return;
            }
            ret = init.Call();
            luaTable.Set<string, bool>("is_init", true);
        }

        public void LuaTest()
        {
            Instance().DoString("require('GEInit')");
        }
        
        private byte[] CustomMyLoader(ref string fileName)
        {
            string luaPath = PathHelp.GetLuaCodePath() + "\\" + fileName + PathHelp.LuaCodeBinEnd;
            string strLuaContent = File.ReadAllText(luaPath);
            byte[] result = System.Text.Encoding.UTF8.GetBytes(strLuaContent);
            return result;
        }
    }
}