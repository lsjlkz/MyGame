using System.Reflection;
using FairyGUI;
using UnityEngine;
using XLua;

namespace CSharp
{
    [LuaCallCSharp]
    public class BindLuaEvent
    {
        public static DelegateEventCallbackLuaProxy BindLuaEventFun(LuaTable owner, GObject go, LuaFunction fun, string eventType)
        {
            DelegateEventCallbackLuaProxy delegateProxy = new DelegateEventCallbackLuaProxy(owner, fun);
            PropertyInfo property = typeof(GObject).GetProperty(eventType);
            if (property == null)
            {
                // 没有这个监听
                GELog.Instance().Log(go.name + " no listener " + eventType);
                return null;
            }
            object o = property.GetValue(go);
            if (o == null)
            {
                // 没有这个监听
                GELog.Instance().Log(go.name + " no listener " + eventType);
                return null;
            }
            EventListener eventListener = (EventListener) o;
            eventListener.Add(delegateProxy.OnEventCallback);
            return delegateProxy;
        }
    }
}