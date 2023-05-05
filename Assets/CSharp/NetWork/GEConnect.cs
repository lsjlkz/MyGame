using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSharp
{
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