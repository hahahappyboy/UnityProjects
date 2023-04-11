using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class 协程测试4 : MonoBehaviour
{
    private static int n = 5;
    void Start(){
        for (int i = 0; i < 10; i++) {
            StartCoroutine(协程1(i));//开启协程
        }
    }
    IEnumerator 协程1(int id){
        if (n==5) {
            n++;
            Debug.Log("id="+id+" n="+n);
        }
        n = 5;
        yield return null;
        Debug.Log("id=" + id + "执行完毕");
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