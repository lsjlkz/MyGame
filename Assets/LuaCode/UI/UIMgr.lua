---
--- Generated by EmmyLua(https://github.com/EmmyLua)
--- Created by lsjlkz.
--- DateTime: 2023/5/11 15:43
---


__G__UIMgrTable = __G__UIMgrTable or {
    has_load_pkg_table = {},
    reg_panel_table = {}
}


function __G__UIMgrTable.load_package(pkg_name)
    if __G__UIMgrTable.has_load_pkg_table[pkg_name] ~= nil then
        return __G__UIMgrTable.has_load_pkg_table[pkg_name]
    end
    local ui_package = CS.CSharp.GEUI.LoadUIPackage(pkg_name)
    if ui_package ~= nil then
        __G__UIMgrTable.has_load_pkg_table[pkg_name] = ui_package
    end
    return ui_package
end

function __G__UIMgrTable.reg_panel(panel_name, panel)
    __G__UIMgrTable.reg_panel_table[panel_name] = panel
end

function __G__UIMgrTable.show_panel(panel_name)
    -- TODO 完整的UI打开和关闭流程
    -- 旧的UI怎么处理，隐藏还是咋滴
    panel = __G__UIMgrTable.reg_panel_table[panel_name]
    if panel == nil then
        print("panel " .. panel_name .. " not reg")
        return
    end
    panel:create()
    panel:after_create()
    panel:show()
    panel:after_show()
end



local function init()
    __G__UIMgrTable.load_package("pub_img")
    __G__UIMgrTable.load_package("pub_com")
end


if __G__UIMgrTable.is_init == nil then
    __G__UIMgrTable.is_init = true
    init()
end



return __G__UIMgrTable