
using UnityEngine;

public class Config {
    
    public static string mainAssetBundleURL  = "http://127.0.0.1:5000/AssetBundles/AssetBundles";//主AB包url
    
    public static string aLLAssetBundleURL  = "http://127.0.0.1:5000/AssetBundles/";//AB包存的路径的url
    public static string luaURL  = "http://127.0.0.1:5000/AssetBundles/Lua/";//AB包存的路径的url
    
    public static string assetBundleSavePath = Application.dataPath+"/DownLoadAssetBundle/";//AB包保存路径
    public static string luaSavePath = Application.dataPath+"/DownLoadAssetBundle/Lua/";//AB包保存路径

}
