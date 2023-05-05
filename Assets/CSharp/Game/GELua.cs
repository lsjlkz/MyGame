using CSharp.Log;
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
            DoString("print(345)");
        }
    }
}