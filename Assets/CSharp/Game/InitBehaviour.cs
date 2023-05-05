using System;
using CSharp.Log;
using FairyGUI;
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
            
            GRoot.inst.SetContentScaleFactor(1334, 750);
            
        }
    }
}