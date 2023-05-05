using System.Collections.Generic;
using FairyGUI;

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
        
        public static GRoot groot()
        {
            return GRoot.inst;
        }

        public static void LoadUIPackage(string pkgName)
        {
            // 加载UI包
            UIPackage.AddPackage($"UI/{pkgName}");
        }

        public void ShowUIPanel(string panelName, string pkgName, string comName)
        {
            GObject gObject;
            if (!panelDict.TryGetValue(panelName, out gObject))
            {
                if (groot().GetChild(gObject.gameObjectName) != null)
                {
                    // 重复添加了
                    return;
                }
                groot().AddChild(gObject);
            }
            gObject = AddUIPanel(pkgName, comName);
            panelDict.Add(panelName, gObject);
        }
        public GObject AddUIPanel(string pkgName, string comName)
        {
            LoadUIPackage(pkgName);
            GObject gObject = UIPackage.CreateObject(pkgName, comName);
            groot().AddChild(gObject);
            return gObject;
        }

        public void HideUIPanel(string panelName)
        {
            
            GObject gObject;
            if (!panelDict.TryGetValue(panelName, out gObject))
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