using System;
using FairyGUI;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace CSharp
{
    public class InitBehaviour:MonoBehaviour
    {
        private void Start()
        {
            
            DontDestroyOnLoad(this);
            
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