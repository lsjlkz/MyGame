
using System;
using UnityEngine;

namespace CSharp
{
    public class GEMilliTime:GESingleton<GEMilliTime>
    {


        private int _milliSeconds = 0;
        private int _lastMilliSeconds = 0;
        
        
        public void Start()
        {
            this.DoUpdateMilliSeconds();
        }

        public void DoUpdateMilliSeconds()
        {
            this._milliSeconds = (int) (Time.realtimeSinceStartupAsDouble * 1000);
        }
        
        public void Update()
        {
            this.DoUpdateMilliSeconds();
            // 由其他的Update触发驱动
            if (this._milliSeconds == this._lastMilliSeconds)
            {
                return;
            }
            this._lastMilliSeconds = this._milliSeconds;
            GESocket.Instance().Update();
        }

        public void DrivenInnernet()
        {
            
        }
    }
}