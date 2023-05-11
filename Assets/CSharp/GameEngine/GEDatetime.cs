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
            this.TriggerPerSecondEvent();
            return true;
        }

        public void TriggerPerSecondEvent()
        {
            // 触发每秒的事件
            this.TriggerLuaPerSecondEvent();
        }

        public void TriggerLuaPerSecondEvent()
        {
            // lua层
            LuaEnv luaEnv = GELua.Instance().GetLuaMainThread();
            LuaTable gevent = luaEnv.Global.Get<LuaTable>("__G__GEventTable");
            if (gevent == null)
            {
                GELog.Instance().Log("TriggerLuaPerSecondEvent:__G__GEventTable is null");
                return;
            }
            int e = gevent.Get<int>("AfterCallPerSecond");
            LuaFunction trigger_event = gevent.Get<LuaFunction>("trigger_event");
            trigger_event.Call(e);
        }

    }
}