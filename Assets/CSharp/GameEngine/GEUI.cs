using System.Collections.Generic;
using FairyGUI;
using XLua;

namespace CSharp
{
    [LuaCallCSharp]
    public class GEUI:GESingleton<GEUI>
    {
        public Dictionary<string, GObject> panelDict = new Dictionary<string, GObject>();



        public static GRoot Groot()
        {
            return GRoot.inst;
        }

        public static void LoadUIPackage(string pkgName)
        {
            // 加载UI包
            UIPackage.AddPackage($"UI/{pkgName}");
        }

        public GObject ShowUIPanel(string panelName, string pkgName, string comName)
        {
            GObject gObject;
            if (!Instance().panelDict.TryGetValue(panelName, out gObject))
            {
                // 无缓存
                gObject = AddUIPanel(pkgName, comName);
                Instance().panelDict.Add(panelName, gObject);
            }
            if(Groot().GetChild(gObject.gameObjectName) != null)
            {
                // 重复添加了
                return gObject;
            }
            return Groot().AddChild(gObject);
        }
        public GObject AddUIPanel(string pkgName, string comName)
        {
            LoadUIPackage(pkgName);
            GObject gObject = UIPackage.CreateObject(pkgName, comName);
            Groot().AddChild(gObject);
            return gObject;
        }

        public void HideUIPanel(string panelName)
        {
            
            GObject gObject;
            if (!Instance().panelDict.TryGetValue(panelName, out gObject))
            {
                if (Groot().GetChild(gObject.gameObjectName) == null)
                {
                    // 居然没有
                    return;
                }
                Groot().RemoveChild(gObject);
            }
        }
    }
}