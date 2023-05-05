using System.Collections.Generic;
using FairyGUI;
using XLua;

namespace CSharp.Game
{
    public class GEUI
    {
        public static GEUI _instance = null;
        public Dictionary<string, GObject> panelDict = new Dictionary<string, GObject>();


        public static GEUI instance()
        {
            if (_instance == null)
            {
                _instance = new GEUI();
            }
            return _instance;
        }

        public void initTable()
        {
            // TODO 这里要想办法解决
            LuaEnv luaEnv = GELua.instance().luaThread();
            LuaTable table = luaEnv.NewTable();
            table.Set("GEUI", this);
            table.Set("__index", this);
            table.SetMetaTable(luaEnv.Global);
            luaEnv.Global.Set("GEUI", table);
        }
        
        public static GRoot groot()
        {
            return GRoot.inst;
        }

        public static void LoadUIPackage(string pkgName)
        {
            // 加载UI包
            UIPackage.AddPackage($"UI/{pkgName}");
        }

        public static GObject ShowUIPanel(string panelName, string pkgName, string comName)
        {
            GObject gObject;
            if (!instance().panelDict.TryGetValue(panelName, out gObject))
            {
                // 无缓存
                gObject = AddUIPanel(pkgName, comName);
                instance().panelDict.Add(panelName, gObject);
            }
            if(groot().GetChild(gObject.gameObjectName) != null)
            {
                // 重复添加了
                return gObject;
            }
            return groot().AddChild(gObject);
        }
        public static GObject AddUIPanel(string pkgName, string comName)
        {
            LoadUIPackage(pkgName);
            GObject gObject = UIPackage.CreateObject(pkgName, comName);
            groot().AddChild(gObject);
            return gObject;
        }

        public static void HideUIPanel(string panelName)
        {
            
            GObject gObject;
            if (!instance().panelDict.TryGetValue(panelName, out gObject))
            {
                if (groot().GetChild(gObject.gameObjectName) == null)
                {
                    // 居然没有
                    return;
                }
                groot().RemoveChild(gObject);
            }
        }
    }
}