---
--- Generated by EmmyLua(https://github.com/EmmyLua)
--- Created by lsjlkz.
--- DateTime: 2023/5/6 17:33
---
---
---
local geconnect = CS.CSharp.GEConnect

local gevent = require("Game/GEvent")
local cDefine = require("Common/CDefine")

__G__ConnectTable = __G__ConnectTable or {}

function __G__ConnectTable.Connect()
--    TODO 需要读文件的host和port
    print("Connect")
    geconnect.Connect("127.0.0.1", cDefine.Port_Gateway)
end

local function init()
    gevent.reg_event(gevent.AfterLoadAllScripts, __G__ConnectTable.Connect)
end

if __G__ConnectTable.is_init ~= true then
    __G__ConnectTable.is_init = true
    init()
end

return __G__ConnectTable