using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotUpdate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AssetBundle ab = AssetBundle.LoadFromFile(Config.ABPath + "/one/image");
        Sprite sprite = ab.LoadAsset<Sprite>("国家队");
        GameObject image = GameObject.Find("Image");
        image.GetComponent<Image>().sprite = sprite;
        ab.Unload(false);
    }

    public void ChangeABAssetBundle() {
        AssetBundle ab = AssetBundle.LoadFromFile(Config.ABPath + "/two/image");
        Sprite sprite = ab.LoadAsset<Sprite>("Eye");
        GameObject image = GameObject.Find("Image");
        image.GetComponent<Image>().sprite = sprite;
        ab.Unload(false);
    }
}
