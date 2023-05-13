using System.Collections.Generic;
using UnityEngine;
using XLua;

namespace CSharp
{
    public class ResourceMgr:GESingleton<ResourceMgr>
    {
        public Dictionary<string, GameObject> ResDictionary;

        public ResourceMgr()
        {
            ResDictionary = new Dictionary<string, GameObject>();
        }

        public bool LoadPrefab(string path, ref GameObject gameObject)
        {
            string prefabPath =  "Prefabs\\" + path;
            gameObject = Resources.Load<GameObject>(prefabPath);
            if (gameObject == null)
            {
                GELog.Instance().Log($"ErrorLoadPrefab{path}");
                return false;
            }

            return true;
        }
        
        public void LoadPrefabAsync(string path, LuaTable self, LuaFunction callback)
        {
            string prefabPath =  "Prefabs\\" + path;
            ResourceRequest request = Resources.LoadAsync(prefabPath, typeof(GameObject));
            request.completed += (o =>
            {
                GameObject prefabObject = Resources.Load<GameObject>(prefabPath);
                GameObject gameObject = GameObject.Instantiate(prefabObject);
                callback.Call(self, gameObject);
            });
        }

    }
}