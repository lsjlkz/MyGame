using System;
using FairyGUI;
using XLua;

namespace CSharp
{
    public class FairyGUIDelegate
    {
        
    }

    [LuaCallCSharp]
    public class DelegateEventCallbackLuaProxy
    {
        // 一个事件委托的代理
        private Action<EventContext> _action;
        private LuaTable _owner;
        public long LongData1;
        public string StrData1;

        public DelegateEventCallbackLuaProxy(LuaTable owner, LuaFunction callback, long param1, string param2)
        {
            _owner = owner;
            LongData1 = param1;
            StrData1 = param2;
            _action = (context) =>
            {
                callback.Call(owner, context);
            };
        }

        public void OnEventCallback(EventContext ctx)
        {
            // TODO 这里Lua和CS是同步的？ 【待确认】
            _owner.Set("param1", LongData1);
            _owner.Set("param2", StrData1);
            _action.Invoke(ctx);
        }
    }
}