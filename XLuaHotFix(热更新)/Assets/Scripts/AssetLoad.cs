using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;
using XLua;
using Object = UnityEngine.Object;
[Hotfix]
public class AssetLoad : MonoBehaviour
{
    private Dictionary<string, GameObject> prefabsDictionary = new Dictionary<string, GameObject>();

    public Button hotFixButton;
    public Button downLoadButton;
    public Button startGame;
    public GameObject plane;

    private void Awake() {
        hotFixButton.onClick.AddListener(HotFixOnClick);
        downLoadButton.onClick.AddListener(DownLoadClick);
        startGame.onClick.AddListener(StartGameClick);
    }

    

    [LuaCallCSharp]
    void Start()
    {
        
    }
    
    #region 下载并保存AB包
    public void DownLoadAssetBundle() {
        StartCoroutine(WWWDownLoadAssetBundle());
    }
    /// <summary>
    /// 从网上下载AB包
    /// </summary>
    /// <returns></returns>
    IEnumerator WWWDownLoadAssetBundle() {
        //下载主AB包
        UnityWebRequest request = UnityWebRequest.Get(Config.mainAssetBundleURL);
        yield return request.SendWebRequest();
        Debug.Log("下载主AB包："+Config.mainAssetBundleURL);
        //获取主AB包
        byte[] mainAbByte = request.downloadHandler.data;
        AssetBundle ab = AssetBundle.LoadFromMemory(mainAbByte);
        SaveAssetBundle2(Path.GetFileName(Config.mainAssetBundleURL), request);
        
        if (ab == null) {
            Debug.Log("not ab");
        }
        //从主AB包种获取Manifest文件
        AssetBundleManifest manifest = ab.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        //获取这个 manifest 文件中所有的 AssetBundle 的名称信息.
        string[] names = manifest.GetAllAssetBundles();
        for (int i = 0; i < names.Length; i++)
        {
            //下载AssetBundle并保存到本地.
            StartCoroutine(DownLoadAssetBundleAndSave(Config.aLLAssetBundleURL + names[i]));
        }
        ab.Unload(false);
    }
    
    IEnumerator DownLoadAssetBundleAndSave(string url)
    {
        //UnityWebRequestAssetBundle.GetAssetBundle(string uri)使用这个API下载回来的资源它是不支持原始数据访问的.
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        //表示下载状态是否完毕.
        if (request.isDone)
        {
            //使用 IO 技术把这个 request 对象存储到本地.(需要后缀)
            SaveAssetBundle2(Path.GetFileName(url), request);
        }
    }
    private void SaveAssetBundle2(string fileName, UnityWebRequest request)
    {
        if (!Directory.Exists(Config.assetBundleSavePath)) {
            Directory.CreateDirectory(Config.assetBundleSavePath);
        }
        //构造文件流.
        FileStream fs = File.Create(Config.assetBundleSavePath +  fileName);
        //将字节流写入文件里,request.downloadHandler.data可以获取到下载资源的字节流.
        fs.Write(request.downloadHandler.data, 0, request.downloadHandler.data.Length);
        //文件写入存储到硬盘，关闭文件流对象，销毁文件对象.
        fs.Flush();
        fs.Close();
        fs.Dispose();
        Debug.Log(fileName + "下载完毕。"+"路径："+Config.assetBundleSavePath  + fileName);
    }
    #endregion

    #region 下载LUA
    [LuaCallCSharp]
    public void DownLoadLua(string fileName) {
        StartCoroutine(LuaDownLoad(fileName));
    }
    IEnumerator LuaDownLoad(string fileName) {
        UnityWebRequest request = UnityWebRequest.Get(Config.luaURL+fileName);
        yield return request.SendWebRequest();
        if (!Directory.Exists(Config.luaSavePath)) {
            Directory.CreateDirectory(Config.luaSavePath);
        }
        string luaStr = request.downloadHandler.text;
        File.WriteAllText(Config.luaSavePath + fileName,luaStr);
    }
    #endregion 
    
    
    # region 资源加载
    [LuaCallCSharp]
    public GameObject GetGameObjectFromDic(string resName) {
        return prefabsDictionary[resName];
    }
    
    
    //加载资源
    [LuaCallCSharp]
    public void LoadAsset(string abName, string prefabName) {
        //加载主AB包
        AssetBundle mainAsset = AssetBundle.LoadFromFile(Config.assetBundleSavePath+"AssetBundles");
        AssetBundleManifest manifest =mainAsset.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        //找prefab依赖
        string[] dependencies = manifest.GetAllDependencies(abName);
        //加载prefab的依赖AB包
        foreach (var item in dependencies) {
            AssetBundle.LoadFromFile(Config.assetBundleSavePath+item);
        }
        //加载找prefabAB包
        AssetBundle prefabAB = AssetBundle.LoadFromFile(Config.assetBundleSavePath+abName);
        //加载预制体
        GameObject prefabGO = prefabAB.LoadAsset<GameObject>(prefabName);
        prefabsDictionary.Add(prefabName,prefabGO);
        Resources.UnloadUnusedAssets();
    }
    # endregion
    
    public void HotFixOnClick() {
        //启动热更脚本
        LuaManager.DoString("require('Test')");
        Debug.Log("热更完成");
    }
    private void DownLoadClick() {
        DownLoadAssetBundle();
        DownLoadLua("test.lua.txt");
        DownLoadLua("LuaDispose.lua.txt");
    }
    [LuaCallCSharp]
    private void StartGameClick() {
        
    }
}
