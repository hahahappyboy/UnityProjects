using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class 协程测试3 : MonoBehaviour
{
    void Start(){
        Debug.Log("Start开始");
        StartCoroutine(协程1());//开启协程
        Debug.Log("Start结束");
    }
    IEnumerator 协程1(){
        Debug.Log("协程开始");
        for (int i = 0; i < 10; i++) //循环C
        {
            Debug.Log("耗时操作开始");
            UnityWebRequest www = UnityWebRequest.Get("www.baidu.com");
            yield  return www.SendWebRequest();
            Debug.Log("耗时操作结束");
        }
        Debug.Log("协程结束");
    }
 
    // 更新数据
    private int frame = 0;
    void Update(){
        frame++;
        Debug.Log("第"+frame+"帧的"+"Update");
    }
    //晚于更新
    void LateUpdate(){
        Debug.Log("第"+frame+"帧的"+"LateUpdate");
    }
}
