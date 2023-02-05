
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager> {
    //界面的父节点
    private Transform _canvas;
    //界面
    private Stack<UIModuleBase> _uiModuleStack;
    //储存窗口的名字和路径
    private static Dictionary<string, string> uiNamePatchDic;
    //储存窗口名字和缓存
    private static Dictionary<string, GameObject> uiNameGameObjectDic;

    private UIManager() {
        _uiModuleStack = new Stack<UIModuleBase>();
        _canvas = GameObject.Find("Canvas").transform;
        uiNamePatchDic = new Dictionary<string, string>();
        uiNameGameObjectDic = new Dictionary<string, GameObject>();
        
        uiNamePatchDic.Add("MainModule","Prefabs/UIModules/MainModule");
        uiNamePatchDic.Add("BagModule","Prefabs/UIModules/BagModule");
        uiNamePatchDic.Add("EquipMsgModule","Prefabs/UIModules/EquipMsgModule");;
    }
    
    //界面压栈
    public void PushUI(string uiName) {
        //先让本来在栈最上面的ui给暂停
        if (_uiModuleStack.Count > 0) {
            _uiModuleStack.Peek().UIPause();
        }
        //没有该生成过该界面,就生成
        if (!uiNameGameObjectDic.ContainsKey(uiName)) {
            GameObject uiPrefab = GetUIGameObject(uiName);
            //压栈
            _uiModuleStack.Push(uiPrefab.GetComponent<UIModuleBase>());
        }
        //执行进入触发事件
        _uiModuleStack.Peek().UIEnter();
        
    }
    
    //界面出栈
    public void PopUI() {
        if (_uiModuleStack.Count>0) {
            //当前模块离开
            _uiModuleStack.Peek().UIExit();
            _uiModuleStack.Pop();
            if (_uiModuleStack.Count>0) {
                _uiModuleStack.Peek().UIResume();
            }
        }
    }
    
    
    //加载UI的Prefabs
    public  GameObject GetUIGameObject(string uiName)  {
        //找之前有没有加载过该窗口，加载过就直接用
        if (!uiNameGameObjectDic.ContainsKey(uiName)) {
            string path = uiNamePatchDic[uiName];
            GameObject uiPrefab = Resources.Load<GameObject>(path);
            // uiPrefab.AddComponent<UIModuleBase>();
            uiNameGameObjectDic.Add(uiName, Object.Instantiate(uiPrefab, _canvas));
        }
        return uiNameGameObjectDic[uiName];
    }
}
