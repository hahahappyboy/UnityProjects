using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class 协程测试6 : MonoBehaviour
{
    void Start(){
        StartCoroutine(协程1());//开启协程

    }
    IEnumerator 协程1(){
       yield return 抢占支援(); //协程1
    }
    public int 抢占支援()
    {
        临界资源.ThreadMain();
         return 1;
    }
 
}