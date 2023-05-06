using System;
using FairyGUI;
using UnityEngine;

namespace CSharp
{
    public class InitBehaviour:MonoBehaviour
    {
        private void Start()
        {
            
            DontDestroyOnLoad(this);
            
            GRoot.inst.SetContentScaleFactor(1334, 750);
            GELua.Instance().InitLuaMainThread();
            GELog.Instance().InitGELog();

            GELua.Instance().LuaTest();
            
            GEMilliTime.Instance().Start();
            
        }

        private void Update()
        {
            GEMilliTime.Instance().Update();
        }
    }
}