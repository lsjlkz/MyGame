using System;

namespace CSharp
{
    public class GESingleton<T> where T:class
    {
        private static T _instance = null;
        
        private static readonly object _lock = new Object();

        public static T Instance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    _instance = (T) Activator.CreateInstance(typeof(T), true);
                }
            }

            return _instance;
        }
    }
    
}