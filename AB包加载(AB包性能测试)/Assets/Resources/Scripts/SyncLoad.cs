using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SyncLoad : MonoBehaviour
{
    void Start() {
        //同步加载
        // GameObject.Find("Canvas/Cube/Image").transform.GetComponent<Image>().sprite =
        //     Resources.Load<Sprite>("Sprites/Eye");
        
        //异步加载
        StartCoroutine(LoadImage());

    }

    IEnumerator LoadImage() {
        ResourceRequest resourceRequest = Resources.LoadAsync<Sprite>("Sprites/Eye");
        yield return resourceRequest;
        GameObject.Find("Canvas/Cube/Image").transform.GetComponent<Image>().sprite = (Sprite)resourceRequest.asset;
    }

  
}
