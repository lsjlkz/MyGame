using System;
using UnityEngine;
using XLua;

namespace CSharp
{
    [LuaCallCSharp]
    public class LuaBehaviour:MonoBehaviour
    {
        private LuaTable _luaTable = null;
        private Action _luaStart;
        private Action _luaUpdate;
        private Action _luaOnDestory;

        private int _lastUpdateSeconds = 0;
        private int _deletaUpdateSeconds = 1;

        // TODO 现在的脚本都是放到同一个luaEnv中，看情况需要放到单独的luaEnv中
        public LuaBehaviour()
        {
            
        }

        public LuaBehaviour(LuaTable luaTable)
        {
            this._luaTable = luaTable;
        }

        public LuaBehaviour(string path)
        {
            LuaTable luaTable = GELua.Instance().LoadTable(path);
            this._luaTable = luaTable;
        }

        public void SetLuaTable(LuaTable luaTable)
        {
            luaTable.Set("__index", luaTable);
            luaTable.SetMetaTable(luaTable);
            
            luaTable.Set("self", this);
            
            this._luaStart = luaTable.Get<Action>("Start");
            this._luaUpdate = luaTable.Get<Action>("Update");
            this._luaOnDestory = luaTable.Get<Action>("OnDestroy");
            Action luaAwake = luaTable.Get<Action>("Awake");
            
            if (luaAwake != null)
            {
                luaAwake();
            }
        }
        
        private void Start()
        {
            if (_luaStart != null)
            {
                _luaStart();
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
            _luaUpdate();
        }

        private void OnDestroy()
        {
            if (_luaOnDestory != null)
            {
                _luaOnDestory();
            }

            if (_luaTable != null)
            {
                _luaTable.Dispose();
                _luaTable = null;
            }
        }
    }
}