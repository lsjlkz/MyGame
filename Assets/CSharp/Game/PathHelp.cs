using XLua;

namespace CSharp
{
    [LuaCallCSharp]
    public class PathHelp
    {
        public static string BinEnd = ".bin";
        public static string ResourcesPath = "Resources";
        public static string LuaCodeBinEnd = ".lua" + BinEnd;
        public static string LuaCodeRelation = $"/{ResourcesPath}/LuaCodeBin";
        public static string PrefabsPath = $"/{ResourcesPath}/Prefabs";

        public static string GetLuaCodePath()
        {
#if UNITY_EDITOR
            return UnityEngine.Application.dataPath + LuaCodeRelation;
#endif
            return UnityEngine.Application.dataPath + LuaCodeRelation;
        }
    }
}