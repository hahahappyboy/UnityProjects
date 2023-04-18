using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        AssetBundle ab = AssetBundle.LoadFromFile(Config.ABPath + "/image");
        Sprite sprite = ab.LoadAsset<Sprite>("国家队");
        GameObject image = GameObject.Find("Canvas/Image");
        image.GetComponent<Image>().sprite = sprite;
    }
}
