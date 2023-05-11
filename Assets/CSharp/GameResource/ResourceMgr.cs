using System.Collections.Generic;
using UnityEngine;

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

    }
}