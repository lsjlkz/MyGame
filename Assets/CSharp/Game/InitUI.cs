using System;
using FairyGUI;
using UnityEngine;

namespace CSharp
{
    public class InitUI:MonoBehaviour
    {
        private void Start()
        {
            GRoot.inst.SetContentScaleFactor(1334, 750);
            DontDestroyOnLoad(this);
        }

    }
}