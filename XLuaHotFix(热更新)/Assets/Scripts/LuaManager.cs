using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using XLua;
using XLua.LuaDLL;

public class LuaManager : MonoBehaviour {
    private static LuaEnv _luaEnv;
    void Start() {
        _luaEnv = new LuaEnv();
        _luaEnv.AddLoader(MyLoader);
        
    }
    public static void DoString(string str) {
        _luaEnv.DoString(str);
    }
    
    
    
    private byte[] MyLoader(ref string filePath) {
        string absPath = Application.dataPath + "/DownLoadAssetBundle/Lua/" + filePath + ".lua.txt";
        return System.Text.Encoding.UTF8.GetBytes(File.ReadAllText(absPath));
    }
    
    
 
    private void OnDestroy() {
        _luaEnv.Dispose();
    }
    private void OnDisable() {
        _luaEnv.DoString("require('LuaDispose')");
    }
}
