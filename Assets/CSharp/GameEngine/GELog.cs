using System;
using UnityEngine;

namespace CSharp
{
    public class GELog:GESingleton<GELog>
    {
        private string filePath;
        
        public void InitGELog()
        {
            // TODO 初始化Log路径等
        }

        public void Log(string value)
        {
            // TODO
            Debug.Log(value);
            // Console.WriteLine(value);
        }
        public void Log(object value)
        {
            // Console.WriteLine(value);
            Debug.Log(value);
        }

        public void Log(string format, object args0)
        {
            Console.WriteLine(format, args0);
        }

        public void Log(string format, object args0, object args1)
        {
            Console.WriteLine(format, args0, args1);
        }

        public void Log(string format, object args0, object args1, object args2)
        {
            Console.WriteLine(format, args0, args1, args2);
        }
    }
}