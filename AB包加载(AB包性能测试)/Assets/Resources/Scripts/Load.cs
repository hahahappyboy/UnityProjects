using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //加载主AB包
        AssetBundle main = AssetBundle.LoadFromFile(Config.ABPath+"/ab");
        //加载主AB包的manifest
        AssetBundleManifest manifest = main.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        //找prefab依赖
        string[] dependencies = manifest.GetAllDependencies("prefab");
        //加载prefab的依赖AB包
        foreach (var item in dependencies) {
            AssetBundle.LoadFromFile(Config.ABPath+"/"+item);
        }
        //加载找prefabAB包
        AssetBundle prefab = AssetBundle.LoadFromFile(Config.ABPath+"/prefab");
        //加载预制体
        GameObject imagePrefab = prefab.LoadAsset<GameObject>("Image");
        imagePrefab = Instantiate(imagePrefab);
        imagePrefab.transform.SetParent(GameObject.Find("/Canvas").transform);
    }

}
