using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsController : MonoBehaviour {
    //level的Trans组件
    private Transform levelTransform;
    //Data实例
    private Dictionary<int, int> starDic;
    
    //总关卡数
    private int LEVEL_NUM = 10;

    private void Awake() {
        starDic = SceneDataManager.GetInstance().StarDic;
    }

    void Start() {
        InitLevel();
        ChangeLevelStar();

    }

    private void ChangeLevelStar() {
        for (int i = 0; i < starDic.Count; i++) {
            Transform level = this.transform.GetChild(i);
            level.Find("Lock").gameObject.SetActive(false);
            level.GetComponent<Button>().interactable = true;
            
            Transform stars = level.Find("Stars");
            //星星激活
            stars.gameObject.SetActive(true);

            switch (starDic[i]) {
                case 0:
                    stars.GetChild(0).gameObject.SetActive(false);
                    stars.GetChild(1).gameObject.SetActive(false);
                    stars.GetChild(2).gameObject.SetActive(false);
                    break;
                case 1:
                    stars.GetChild(0).gameObject.SetActive(false);
                    stars.GetChild(1).gameObject.SetActive(false);
                    stars.GetChild(2).gameObject.SetActive(true);
                    break;
                case 2:
                    stars.GetChild(0).gameObject.SetActive(true);
                    stars.GetChild(1).gameObject.SetActive(false);
                    stars.GetChild(2).gameObject.SetActive(true);
                    break;
                case 3:
                    stars.GetChild(0).gameObject.SetActive(true);
                    stars.GetChild(1).gameObject.SetActive(true);
                    stars.GetChild(2).gameObject.SetActive(true);
                    break;
                default:
                    break;
            }
        }
        //
        if (starDic.Count != LEVEL_NUM && starDic.Count != 0) {
            Transform level = this.transform.GetChild(starDic.Count);
            level.Find("Lock").gameObject.SetActive(false);
            level.GetComponent<Button>().interactable = true;
        }
    }

    private void InitLevel() {
        for (int i = 0; i < this.transform.childCount; i++) {
            levelTransform = this.transform.GetChild(i).transform;
            //关卡数
            levelTransform.GetChild(0).GetComponent<Text>().text = (i + 1).ToString();
            //星关了
            levelTransform.GetChild(1).gameObject.SetActive(false);
            //按钮不可用
            levelTransform.GetComponent<Button>().interactable = false;
            //第一关把锁关了
            if (i==0) {
                levelTransform.GetChild(1).gameObject.SetActive(true);
                levelTransform.GetChild(2).gameObject.SetActive(false);
                levelTransform.GetChild(3).gameObject.SetActive(false);
                levelTransform.GetComponent<Button>().interactable = true;
            }
            //设置自己是第几个关卡
            levelTransform.GetComponent<LevelController>().LevelNum = i;
            
        }
        
    }

    
}
