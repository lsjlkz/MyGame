---
--- Created by lsjlkz.
--- DateTime: 2022/6/1 17:37
--- Desc:
---

__G__Module_Table = __G__Module_Table or {}

function __G__Module_Table.get_lua_root_folder()
    return CS.UnityEngine.Application.dataPath .. "\\Resources\\LuaCodeBin\\"
end

function __G__Module_Table.findindir(path, wefind, r_table, intofolder)
    paths = CS.CSharp.LuaHelp.GetDirectories(path)
    for i, file in ipairs(paths) do
        if file ~= "." and file ~= ".." then
            local f = path..'\\'..file
            if string.find(f, wefind) ~= nil then
                table.insert(r_table, f)
            end
            local files = CS.CSharp.LuaHelp.GetFiles(f)
            for i, f in ipairs(files) do
                local fn = CS.System.IO.Path.GetFileName(f)
                local idx = string.find(fn, "LuaCodeBin")
                local s = string.sub(fn, idx + 11, -9)
                local l = require(s)
                if type(l) == "table" then
                    if l['init'] ~= nil then
                        l['init']()
                    end
                end
            end
            local dirs = CS.CSharp.LuaHelp.GetDirectories(f)
            for i, dir in ipairs(dirs) do
                __G__Module_Table.findindir (dir, wefind, r_table, intofolder)
            end
        end
    end
end


function __G__Module_Table.load_all_module(package_name)
    currentFolder = __G__Module_Table.get_lua_root_folder()
    currentFolder = currentFolder .. package_name
    input_table = {}
    __G__Module_Table.findindir(currentFolder, "%.lua.bin", input_table, true)--查找lua文件，这里可以改的
end

return __G__Module_Table