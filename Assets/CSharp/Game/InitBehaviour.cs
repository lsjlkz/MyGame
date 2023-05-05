using System;
using CSharp.Log;
using UnityEngine;

namespace CSharp.Game
{
    public class InitBehaviour:MonoBehaviour
    {
        private void Start()
        {
            GELua.instance().initLuaThread();
            GELog.instance().initGELog();
            
            DontDestroyOnLoad(this);
            
        }
    }
}