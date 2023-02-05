using System.Collections.Generic;
using UnityEngine;

public static class SystemDefine {
    //储存窗口的名字和路径
    private static Dictionary<string, string> uiNamePatchDic;
    //储存窗口名字和缓存
    private static Dictionary<string, Object> uiNameGameObjectDic;

    static SystemDefine() {
        uiNamePatchDic = new Dictionary<string, string>();
        uiNameGameObjectDic = new Dictionary<string, Object>();
        uiNamePatchDic.Add("MainModule","Prefabs/UIModules/MainModule");
        uiNamePatchDic.Add("BagModule","Prefabs/UIModules/BagModule");
        uiNamePatchDic.Add("EquipMsgModule","Prefabs/UIModules/EquipMsgModule");;
    }

    public static T GetWindowAssets<T>(string windowName) where T : Object {
        //找之前有没有加载过该窗口，加载过就直接用
        if (!uiNameGameObjectDic.ContainsKey(windowName)) {
            string path = uiNamePatchDic[windowName];
            uiNameGameObjectDic.Add(windowName,Resources.Load<T>(path));
        }
        return uiNameGameObjectDic[windowName] as T;
    }
}
