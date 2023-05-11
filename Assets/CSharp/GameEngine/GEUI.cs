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

        public static UIPackage LoadUIPackage(string pkgName)
        {
            // 加载UI包
            return UIPackage.AddPackage($"UI/{pkgName}");
        }

        public static GObject CreateUIPanel(string panelName, string pkgName, string comName)
        {
            GObject gObject;
            if (!Instance().panelDict.TryGetValue(panelName, out gObject))
            {
                // 无缓存
                gObject = UIPackage.CreateObject(pkgName, comName);
                gObject.gameObjectName = panelName;
                Instance().panelDict.Add(panelName, gObject);
            }
            return gObject;
        }
        
        public static bool ShowUIPanel(string panelName)
        {
            GObject gObject;
            if (!Instance().panelDict.TryGetValue(panelName, out gObject))
            {
                return false;
            }
            Groot().AddChild(gObject);
            return true;
            
        }
        public static void HideUIPanel(string panelName)
        {
            GObject gObject = Groot().GetChild(panelName);
            if (gObject == null)
            {
                // 居然没有
                return;
            }
            Groot().RemoveChild(gObject);
        }
    }
}