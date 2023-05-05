using System;
using FairyGUI;
using UnityEngine;

namespace CSharp.Game
{
    public class InitUI:MonoBehaviour
    {
        private void Start()
        {
            DontDestroyOnLoad(this);
            
            
            GEUI.AddUIPanel("Basics", "Main");
        }

    }
}