using System;
using FairyGUI;
using XLua;

namespace CSharp
{
    [LuaCallCSharp]
    public class BindLuaEvent
    {
        public static DelegateEventCallbackLuaProxy BindLuaEventFun(LuaTable owner, GComponent go, LuaFunction fun)
        {
            DelegateEventCallbackLuaProxy clickDelegate = new DelegateEventCallbackLuaProxy(owner, fun);
            go.onClick.Add(clickDelegate.OnEventCallback);
            return clickDelegate;
        }
    }
}