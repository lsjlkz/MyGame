using XLua;

namespace CSharp
{
    [LuaCallCSharp]
    public class GEConnect
    {
        private static GEConnect _instance = null;
        
        public static GEConnect Instance()
        {
            if(_instance == null)
            {
                _instance = new GEConnect();
            }
            return _instance;
        }
    }

}