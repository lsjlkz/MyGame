---
--- Generated by EmmyLua(https://github.com/EmmyLua)
--- Created by lsjlkz.
--- DateTime: 2023/5/5 15:58
---

local uibase = require("UI/UIBase/UIBase")

__MainPanelTable__ = __MainPanelTable__ or uibase:new("MainPanel", "Basics", "Main")

function __MainPanelTable__:after_create()
    self.btn_Button = self:get_child("btn_Button")
end

function __MainPanelTable__:after_show()
    print(self.btn_Button)
    self.btn_Button.title = "测试"
end


return __MainPanelTable__