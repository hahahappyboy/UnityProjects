using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class CreateAssetBundle 
{

   [MenuItem("Assets/BuildAssetBundle")]
   static void BuildAssetBundle() {
      string dir = Application.dataPath+"/AssetBundles";
      if (Directory.Exists(dir)==false) {
         Directory.CreateDirectory(dir);
      }
      //dir导出路径，None为默认压缩格式，使用Windows平台
      BuildPipeline.BuildAssetBundles(dir, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
   }
   
   
}
