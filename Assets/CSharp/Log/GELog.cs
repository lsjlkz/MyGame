using System;

namespace CSharp.Log
{
    public class GELog
    {
        private string filePath;
        private static GELog _instance = null;
        
        public static GELog instance()
        {
            if (_instance == null)
            {
                _instance = new GELog();
            }
            return _instance;
        }

        public void initGELog()
        {
            // TODO 初始化Log路径等
        }

        public void Log(string value)
        {
            // TODO
            Console.WriteLine(value);
        }
        public void Log(object value)
        {
            Console.WriteLine(value);
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