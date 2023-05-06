using System;
using FairyGUI;
using UnityEngine;

namespace CSharp
{
    public class InitUI:MonoBehaviour
    {
        private void Start()
        {
            DontDestroyOnLoad(this);
        }

    }
}