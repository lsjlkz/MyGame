using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSharp
{
    public class GEConnect : MonoBehaviour
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
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }

}