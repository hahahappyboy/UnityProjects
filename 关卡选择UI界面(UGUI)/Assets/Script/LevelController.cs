using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour {
    //是第几关卡
    public int levelNum;

    private Transform selectFrame;
    private void Awake() {
        //获取选择框组件
        selectFrame = GameObject.FindWithTag("SelectFrame").transform;
    }

    public int LevelNum {
        get { return levelNum; }
        set { levelNum = value; }
    }

    public void MoveSelectFrame() {
        // Debug.Log(LevelNum);
        if (this.GetComponent<Button>().interactable) {
            selectFrame.SetParent(this.transform,false);
            selectFrame.GetComponent<SelectFrameController>().SelectNum = LevelNum;
        }
    }
}
