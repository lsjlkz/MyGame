using System.IO;
using CSharp;
using UnityEditor;
using UnityEngine;

namespace Tools
{
    public class LuaCodeBin
    {
        // 生成luacode 的bin文件
        public static string GetLuaCodePath()
        {
           return Application.dataPath + "/LuaCode";
        }

        
        [MenuItem("Lua/ExportLuaCodeBin")]
        private static void BuildLuaCodeBin()
        {
            string binPath = PathHelp.GetLuaCodePath();
            if (Directory.Exists(binPath))
            {
                Directory.Delete(binPath, true);
            }
            Directory.CreateDirectory(binPath);
            WriteLuaCodeDir(GetLuaCodePath(), binPath);
            Debug.Log("Finish Export Lua Code Bin");
        }

        private static void WriteLuaCodeDir(string dirPath, string targetDirPath)
        {
            string[] files = Directory.GetFiles(dirPath);
            string[] dirs = Directory.GetDirectories(dirPath);
            
            if (Directory.Exists(targetDirPath))
            {
                Directory.Delete(targetDirPath, true);
            }
            Directory.CreateDirectory(targetDirPath);
            
            foreach(string file in files)
            {
                string fileName = Path.GetFileName(file);
                if (!fileName.EndsWith(".lua"))
                {
                    continue;
                }
                WriteLuaCodeFile($"{dirPath}/{fileName}", $"{targetDirPath}/{fileName}{PathHelp.BinEnd}");
            }

            foreach (string dir in dirs)
            {
                DirectoryInfo diri = new DirectoryInfo(dir);
                string dirName = diri.Name;
                WriteLuaCodeDir($"{dirPath}/{dirName}", $"{targetDirPath}/{dirName}");
            }
        }

        private static void WriteLuaCodeFile(string filePath, string targetFilePath)
        {
            byte[] buf;
            using (FileStream fos = File.Create(targetFilePath))
            {
                foreach (string line in File.ReadAllLines(filePath))
                {
                    // 不要注释
                    if (line.Trim().StartsWith("--"))
                    {
                        continue;
                    }

                    if (line.Trim().Equals(""))
                    {
                        continue;
                    }
                    buf = System.Text.Encoding.UTF8.GetBytes(line + "\n");
                    fos.Write(buf, 0, buf.Length);
                }
            }
        }
    }
}