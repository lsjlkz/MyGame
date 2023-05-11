
using System;
using UnityEngine;

namespace CSharp
{
    public class GEMilliTime:GESingleton<GEMilliTime>
    {


        private int _clientStartUpMilliSeconds = 0;
        private int _lastclientStartUpMilliSeconds = 0;
        
        
        public void Start()
        {
            this.DoUpdateMilliSeconds();
            GEDatetime.Instance().Start();
        }

        public int GetClientStartUpMilliSeconds()
        {
            return this._clientStartUpMilliSeconds;
        }

        public void DoUpdateMilliSeconds()
        {
            this._clientStartUpMilliSeconds = GETime.GetMilliSecondsSinceStartUp();
        }
        
        public void Update()
        {
            this.DoUpdateMilliSeconds();
            // 由其他的Update触发驱动
            if (this._lastclientStartUpMilliSeconds == this._clientStartUpMilliSeconds)
            {
                return;
            }
            this._lastclientStartUpMilliSeconds = this._clientStartUpMilliSeconds;
            GEDatetime.Instance().Update();
            GESocket.Instance().Update();
        }

        public void DrivenInnernet()
        {
            
        }
    }
}