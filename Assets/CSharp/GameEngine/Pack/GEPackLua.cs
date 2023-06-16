using System;
using System.Runtime.InteropServices;
using UnityEngine;
using XLua;
using XLua.LuaDLL;

#pragma warning disable CS0162 // 消除未使用的代码警告
#pragma warning disable CS0219 // 消除未使用的变量警告
#pragma warning disable CS0649 // 消除未初始化字段警告
#pragma warning disable IDE0051 // 消除未使用的私有成员警告


using LuaAPI = XLua.LuaDLL.Lua;
namespace CSharp
{
    
    // luapack库对象的struct
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct LuaPack
    {
        public System.UInt64 WriteSize;
        public System.UInt64 ReadSize;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = GENetDefine.LUA_MAX_BUF_SIZE)]
        public byte[] Buf;
    }
    
    [LuaCallCSharp]
    public class GEPackLua
    {

        public static string PackPackPack = "__G__LuaPackObj";

        private static LuaPack LuaPackObj;
        private static IntPtr LuaPackObjPtr;
        private static UInt16 MsgType;
        public static int InitPackPack()
        {
            // 这个太麻烦了，貌似xlua不支持userdata，也不能像C一样直接使用lua源码的定义的struct，只能这样子调用
            LuaEnv luaEnv = GELua.Instance().GetLuaMainThread();
            var L = luaEnv.L;
            // 压入lua.pack
            LuaAPI.xlua_getglobal(L, "lua.pack");
            // 压入函数名字
            LuaAPI.lua_pushstring(L, "new");
            // 这个操作是获取栈顶名字new的值，也就是t['new']
            LuaAPI.xlua_pgettable(L, -2);
            // 此时栈中是lua.pack， lua.pack.new
            // 弹出函数,此时栈中数量为2，lua.pack，userdata
            int err = LuaAPI.lua_pcall(L, 0, 1, 0);
            if (err != 0)
            {
                return err;
            }
            LuaPackObjPtr = LuaAPI.lua_touserdata(L, -1);
            err = LuaAPI.xlua_setglobal(L, PackPackPack);
            if (err != 0)
            {
                return err;
            }
            LuaAPI.lua_remove(L, -1);
            return 0;
        }
        
        
        public static int PackObj(int msgType, LuaTable luaTable)
        {
            // msgType在栈1
            // luaTable在栈2
            MsgType = (UInt16)msgType;
            LuaEnv luaEnv = GELua.Instance().GetLuaMainThread();
            var L = luaEnv.L;
            // 压入lua.pack
            LuaAPI.xlua_getglobal(L, "lua.pack");
            // 压入函数名字
            LuaAPI.lua_pushstring(L, "pack");
            // 这个操作是获取栈顶名字pack的值，也就是t['pack']
            LuaAPI.xlua_pgettable(L, -2);
            LuaAPI.xlua_getglobal(L, PackPackPack);
            LuaAPI.lua_pushvalue(L, 2);
            int ret = LuaAPI.lua_pcall(L, 2, 0, 0);
            if (ret == 0)
            {
                // 打包成功了
                LuaPackObj = Marshal.PtrToStructure<LuaPack>(LuaPackObjPtr);
            }
            return ret;
        }

        public static void WriteToLuaPack(LuaPack src)
        {
            IntPtr srcPtr = Marshal.AllocHGlobal(Marshal.SizeOf<LuaPack>());
            Marshal.StructureToPtr<LuaPack>(src, srcPtr, false);
            Marshal.Copy(srcPtr, new IntPtr[1]{LuaPackObjPtr}, 0, (int)(src.WriteSize + 4));
            // for (int i = 0; i < Marshal.SizeOf<LuaPack>(); i++)
            // {
                // Marshal.WriteByte(LuaPackObjPtr, i, Marshal.ReadByte(srcPtr, i));
            // }
            Marshal.FreeHGlobal(srcPtr);
        }

        public static int PackMsg()
        {
            // 获取长度
            return (int) LuaPackObj.WriteSize;
        }


        public static int SendPackMsg()
        {
            // 发送到服务器
            if(!GESocket.Instance().IsConnect())
            {
                GELog.Instance().Log("err socket disconnect");
                return 0;
            }

            UInt16 WriteSize = (UInt16)LuaPackObj.WriteSize;
            GESocket.Instance().WriteMsg(BitConverter.GetBytes(MsgType), sizeof(UInt16));
            // 把消息长度打包进去，其中这个 8 是消息头长度
            GESocket.Instance().WriteMsg(BitConverter.GetBytes(WriteSize + 8), sizeof(UInt16));
            
            // TODO 4个字节的重定向
            GESocket.Instance().WriteMsg(BitConverter.GetBytes(0), 4);
            GESocket.Instance().WriteMsg(LuaPackObj.Buf, WriteSize);
            GESocket.Instance().SendMsg();
            return 1;
        }
        
    }
}