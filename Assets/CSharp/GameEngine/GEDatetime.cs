using UnityEngine;
using XLua;

namespace CSharp
{
    [LuaCallCSharp]
    public class GEDatetime:GESingleton<GEDatetime>
    {
        // 本地启动时间
        private int _localMachineStartUpSeconds = 0;
        // 本地已经过的时间
        private int _localMachineSecondsSinceStartUp = 0;
        
        // 服务器时间
        private int _serverSeconds = 0;
        private int _lastUpdateServerSeconds = 0;

        public GEDatetime()
        {
        }

        public void SetServerSeconds(int severSeconds)
        {
            this._serverSeconds = severSeconds;
        }


        public int GetServerSeconds()
        {
            int passSeconds = this.GetLocalMachineSecondsSinceStartUp() - _lastUpdateServerSeconds;
            return this._serverSeconds + passSeconds;
        }
        public int GetLocalMachineSecondsSinceStartUp()
        {
            return this._localMachineSecondsSinceStartUp;
        }

        public void Update()
        {
            if (!DrivenUpdateSeconds())
            {
                return;
            }
            GELua.Instance().Tick();
        }

        public void Start()
        {
            this._localMachineStartUpSeconds = GETime.GetTimestamp();
            this._localMachineSecondsSinceStartUp = 0;

            // TODO 
            this._serverSeconds = _localMachineStartUpSeconds;
            DrivenUpdateSeconds();
        }

        public bool DrivenUpdateSeconds()
        {
            int t = GETime.GetSecondsSinceStartUp();
            if (this.GetLocalMachineSecondsSinceStartUp() == t)
            {
                return false;
            }
            this._localMachineSecondsSinceStartUp = t;
            return true;
        }
        

    }
}