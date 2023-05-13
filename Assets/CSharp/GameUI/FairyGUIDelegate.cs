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

        public DelegateEventCallbackLuaProxy(LuaTable owner, LuaFunction callback)
        {
            _action = (context) =>
            {
                callback.Call(owner, context);
            };
        }

        public void OnEventCallback(EventContext ctx)
        {
            _action.Invoke(ctx);
        }
    }
}