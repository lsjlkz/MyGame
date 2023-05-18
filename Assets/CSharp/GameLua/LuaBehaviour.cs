using System;
using UnityEngine;
using XLua;

namespace CSharp
{
    [LuaCallCSharp]
    public class LuaBehaviour:MonoBehaviour
    {
        private LuaTable owner = null;
        private LuaFunction _luaStart = null;
        private LuaFunction _luaUpdate = null;
        private LuaFunction _luaOnDestory = null;

        private int _lastUpdateSeconds = 0;
        private int _deletaUpdateSeconds = 1;

        // TODO 现在的脚本都是放到同一个luaEnv中，看情况需要放到单独的luaEnv中
        public LuaBehaviour()
        {
        }

        public LuaBehaviour(LuaTable _owner)
        {
            SetLuaTable(_owner);
        }

        public LuaBehaviour(string path)
        {
            LuaTable luaTable = GELua.Instance().LoadTable(path);
            SetLuaTable(luaTable);
        }

        private void Awake()
        {
            this.gameObject.SetActive(false);
        }

        public void SetLuaTable(LuaTable _owner)
        {
            owner = _owner;
            _owner.Set("__index", GELua.Instance().GetLuaMainThread());
            _owner.SetMetaTable(_owner);
            
            _owner.Set("this", this);
            
            this._luaStart = _owner.Get<LuaFunction>("Start");
            this._luaUpdate = _owner.Get<LuaFunction>("Update");
            this._luaOnDestory = _owner.Get<LuaFunction>("OnDestroy");
            LuaFunction luaAwake = _owner.Get<LuaFunction>("Awake");
            
            if (luaAwake != null)
            {
                luaAwake.Call(owner);
            }
            this.gameObject.SetActive(true);
        }
        
        private void Start()
        {
            if (_luaStart != null)
            {
                _luaStart.Call(owner);
            }
            int gameSeconds = GEDatetime.Instance().GetLocalMachineSecondsSinceStartUp();
            this._lastUpdateSeconds = gameSeconds;
        }

        private void Update()
        {
            if (_luaUpdate == null)
            {
                return;
            }
            int gameSeconds = GEDatetime.Instance().GetLocalMachineSecondsSinceStartUp();
            if (gameSeconds < this._lastUpdateSeconds + this._deletaUpdateSeconds)
            {
                return;
            }
            this._lastUpdateSeconds = gameSeconds;
            _luaUpdate.Call(owner);
        }

        private void OnDestroy()
        {
            if (_luaOnDestory != null)
            {
                _luaOnDestory.Call(owner);
            }

            if (owner != null)
            {
                owner.Dispose();
                owner = null;
            }
        }
    }
}