using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

/// <summary>
/// 在使用c#开发的时候需要后续进行“热补丁修复”的类，需要在类的头部添加一个特性标签：[HotFix],表示该类可以被Xlua热修复，在需要跟新的方法里打上
/// </summary>
[Hotfix]
public class SphereController : MonoBehaviour {
    private Rigidbody _rigidbody;
    void Start() {
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    [LuaCallCSharp]
    // Update is called once per frame
    void Update()
    {
      
    }


  
}
