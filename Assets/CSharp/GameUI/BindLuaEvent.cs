using System.Reflection;
using FairyGUI;
using UnityEngine;
using XLua;

namespace CSharp
{
    [LuaCallCSharp]
    public class BindLuaEvent
    {
        public static DelegateEventCallbackLuaProxy BindLuaEventFun(LuaTable owner, EventListener eventListener, LuaFunction fun, long param1, long param2)
        {
            DelegateEventCallbackLuaProxy delegateProxy = new DelegateEventCallbackLuaProxy(owner, fun, param1, param2);
            eventListener.Add(delegateProxy.OnEventCallback);
            return delegateProxy;
        }
    }
}