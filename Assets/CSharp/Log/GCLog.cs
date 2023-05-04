using System;

namespace CSharp.Log
{
    public class GCLog
    {
        private string filePath;
        private static GCLog _instance = null;
        
        public static GCLog instance()
        {
            if (_instance == null)
            {
                _instance = new GCLog();
            }
            return _instance;
        }

        public void Log(string value)
        {
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