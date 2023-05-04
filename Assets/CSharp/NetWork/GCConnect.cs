using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSharp
{
    public class GCConnect : MonoBehaviour
    {
        private static GCConnect _instance = null;
        
        public static GCConnect Instance()
        {
            if(_instance == null)
            {
                _instance = new GCConnect();
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