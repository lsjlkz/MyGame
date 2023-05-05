using FairyGUI;

namespace CSharp.Game
{
    public class GEUI
    {
        public static GEUI _instance = null;
        

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

        public static void AddUIPanel(string pkgName, string comName)
        {
            LoadUIPackage(pkgName);
            GObject gObject = UIPackage.CreateObject(pkgName, comName);
            groot().AddChild(gObject);
        }
    }
}