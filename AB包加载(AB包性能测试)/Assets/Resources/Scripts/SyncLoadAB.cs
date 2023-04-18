
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SyncLoadAB : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadAB("/Image"));
    }
    IEnumerator LoadAB(string name) {
        AssetBundleCreateRequest request = AssetBundle.LoadFromFileAsync(Config.ABPath + name);
        yield return request;
        GameObject.Find("Canvas/Cube/Image").GetComponent<Image>().sprite =
            request.assetBundle.LoadAsset<Sprite>("国家队");

    }
}
