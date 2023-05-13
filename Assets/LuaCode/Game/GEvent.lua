---
--- Generated by EmmyLua(https://github.com/EmmyLua)
--- Created by lsjlkz.
--- DateTime: 2023/5/6 17:34
---


local cur_event_allot_id = 0

--为什么这里用负数，因为玩家身上存储了正数的索引，当玩家的数据改变后，也会触发事件
local function allot_event_id()
    cur_event_allot_id = cur_event_allot_id - 1
    return cur_event_allot_id
end

local function make_event()
    return allot_event_id()
end

__G__GEventTable = __G__GEventTable or {}


__G__GEventTable._Event_Function = __G__GEventTable._Event_Function or {}
__G__GEventTable._DelayEvent_Function = __G__GEventTable._DelayEvent_Function or {}
__G__GEventTable._DelayEvent_Trigger = __G__GEventTable._DelayEvent_Trigger or {}



__G__GEventTable.AfterLoadAllScripts = make_event()




function __G__GEventTable.reg_event(event_id, func, reg_param)
    if __G__GEventTable._Event_Function[event_id] == nil then
        __G__GEventTable._Event_Function[event_id] = {}
    end
    table.insert(__G__GEventTable._Event_Function[event_id], {func, reg_param})
end

function __G__GEventTable.reg_delay_event(event_id, func, reg_param)
    if __G__GEventTable._DelayEvent_Function[event_id] == nil then
        __G__GEventTable._DelayEvent_Function[event_id] = {}
    end
    table.insert(__G__GEventTable._DelayEvent_Function[event_id], {func, reg_param})
end

function __G__GEventTable.trigger_event(event_id, param1, param2, param3, param4)
    if __G__GEventTable._Event_Function[event_id] == nil then
        return
    end
    for _, v in ipairs(__G__GEventTable._Event_Function[event_id]) do
        func = v[1]
        reg_param = v[2]
        func(reg_param, param1, param2, param3, param4)
    end
end

function __G__GEventTable.trigger_delay_event(event_id, param1, param2, param3, param4)
    if __G__GEventTable._DelayEvent_Function[event_id] == nil then
        return
    end
    __G__GEventTable._DelayEvent_Trigger[event_id] = {event_id, param1, param2, param3, param4}
end

local function call_per_sec_delay_event()
    if __G__GEventTable._DelayEvent_Trigger == {} then
        return
    end
    for event_id, param in pairs(__G__GEventTable._DelayEvent_Trigger) do
        param1 = param[1]
        param2 = param[2]
        param3 = param[3]
        param4 = param[4]
        if __G__GEventTable._DelayEvent_Function[event_id] ~= nil then
            for _, v in ipairs(__G__GEventTable._DelayEvent_Function[event_id]) do
                func = v[1]
                reg_param = v[2]
                func(reg_param, param1, param2, param3, param4)
            end
        end
    end
    __G__GEventTable._DelayEvent_Trigger = {}
end

function __G__GEventTable.init()
    --注册每秒的事件
    __G__GEventTable.reg_event(__G__GEventTable.AfterCallPerSecond, call_per_sec_delay_event)
end

return __G__GEventTable