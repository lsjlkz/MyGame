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
            
            DontDestroyOnLoad(this);
            
            GRoot.inst.SetContentScaleFactor(1334, 750);
            GELua.instance().initLuaThread();
            GELog.instance().initGELog();

            GEUI.instance().initTable();

            GELua.instance().luaTest();
            
        }
    }
}