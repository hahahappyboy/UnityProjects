using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
public class ExportABBundle 
{
    [MenuItem("AB包/导出")]
    private static void ExportAB() {
        string path = Config.ABPath;
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        //dir导出路径，None为默认压缩格式，使用Windows平台
        BuildPipeline.BuildAssetBundles(
            path,
            BuildAssetBundleOptions.None,
            BuildTarget.StandaloneWindows64
        );
        Debug.Log("导出成功:"+path);
    }
    
}
