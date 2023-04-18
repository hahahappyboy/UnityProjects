using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Memory : MonoBehaviour
{
    private void Start() {
        Resources.UnloadUnusedAssets();
    }

    public void LoadImage() {
        Debug.Log("点击");
        //内存上升：但这时候只是个AssetBundle内存镜像数据块，还没有Assets的概念。   
        AssetBundle ab = AssetBundle.LoadFromFile(Config.ABPath+"/Image");
        //内存上升：这才会从AssetBundle的内存镜像里读取并创建一个Asset对象，创建Asset对象同时也会分配相应内存用于存放(反序列化)  
        GameObject.Find("Cube/Canvas/Image").GetComponent<Image>().sprite = ab.LoadAsset<Sprite>("国家队");
        // //
        // ab.Unload(false);
        //
        // // ab.Unload(true);
        //
        // ab.Unload(false);
        // Resources.UnloadUnusedAssets();
        //
        // // Destroy(img.gameObject);
    }

    private AssetBundle ab;
    private Sprite sprite;
    public void LoadAssetBundle() {
        //内存上升：但这时候只是个AssetBundle内存镜像数据块，还没有Assets的概念。   
        ab = AssetBundle.LoadFromFile(Config.ABPath+"/Image");
    }
    public void LoadAsset() {
        sprite = ab.LoadAsset<Sprite>("国家队");
    }

    public void UnLoadFalse() {
        ab.Unload(false);
    }
    
}
