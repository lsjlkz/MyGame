using System;
using UnityEngine;

namespace CSharp
{
    public class GETime
    {
        public static int GetSecondsSinceStartUp()
        {
            return (int) (Time.realtimeSinceStartup);
        }

        public static int GetMilliSecondsSinceStartUp()
        {
            return (int) (Time.realtimeSinceStartup) * 1000;
        }
        
        
        public static int GetTimestamp()
        {
            TimeSpan timeSpan = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (int)timeSpan.TotalSeconds;
        }
    }
}