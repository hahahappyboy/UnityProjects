using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelStarController : MonoBehaviour
{
    //当前的关数
    private int levelNUm = 0;
    //当前的关数Text
    private Text levelHeadText; 
    //用户选择的星
    private int starNumber = 0;
    private void Awake() {
        levelHeadText = GameObject.FindWithTag("LevelHeadText").GetComponent<Text>();
        levelNUm = (int)SceneDataManager.GetInstance().ReadSceneData()["SelectNum"];
    }

    void Start() {
        levelHeadText.text = levelHeadText.text + "-" + (levelNUm+1);
        this.transform.GetChild(0).gameObject.SetActive(false);
        this.transform.GetChild(1).gameObject.SetActive(false);
        this.transform.GetChild(2).gameObject.SetActive(false);
    }



    public void OnStarChangeClick(string num) {
        if(num == "")
            return;
        starNumber = Int32.Parse(num);
        starNumber = Mathf.Clamp(starNumber, 0, 4);
        switch (starNumber) {
            case 0:
                this.transform.GetChild(0).gameObject.SetActive(false);
                this.transform.GetChild(1).gameObject.SetActive(false);
                this.transform.GetChild(2).gameObject.SetActive(false);
                break;
            case 1:
                this.transform.GetChild(0).gameObject.SetActive(false);
                this.transform.GetChild(1).gameObject.SetActive(false);
                this.transform.GetChild(2).gameObject.SetActive(true);
                break;
            case 2:
                this.transform.GetChild(0).gameObject.SetActive(true);
                this.transform.GetChild(1).gameObject.SetActive(false);
                this.transform.GetChild(2).gameObject.SetActive(true);
                break;
            case 3:
                this.transform.GetChild(0).gameObject.SetActive(true);
                this.transform.GetChild(1).gameObject.SetActive(true);
                this.transform.GetChild(2).gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void OnOKButtonClick() {
        Dictionary<string, object> param = new Dictionary<string, object>();
        param.Add("starNumber", starNumber);
        //传给单例
        SceneDataManager.GetInstance().StarDic[levelNUm]=starNumber;
        //返回水平场景
        SceneDataManager.GetInstance().ToNewScene("LevelSelectScene", param);
    }
    
    
    
    
}
