using System;
using FairyGUI;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace CSharp
{
    public class InitBehaviour:MonoBehaviour
    {
        private void Start()
        {
#if UNITY_EDITOR
            // 如果是编辑器模式的话，那就先build一下
            Tools.LuaCodeBin.BuildLuaCodeBin();
#endif
            
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