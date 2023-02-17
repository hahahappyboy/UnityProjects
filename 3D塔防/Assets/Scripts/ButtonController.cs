using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour {

    private List<Image> towerButton;
    private int currentChioceTowrtID = -1;
    private RaycastHit _raycastHit;

    private GameObject VictoryImage; 
    
    [Header("炮塔")]
    public GameObject[] towerPrefab;

    private static ButtonController intance;
    
    public static ButtonController GetInstance() {
        return intance;
    }
    
    private void Awake() {
        intance = this;
        towerButton = new List<Image>();
        VictoryImage = this.transform.GetChild(1).gameObject;
        VictoryImage.SetActive(false);
        
    }

    void Start()
    {   
        //获取画布下的按钮
        for (int i = 0; i < this.transform.GetChild(0).childCount; i++) {
            towerButton.Add(this.transform.GetChild(0).GetChild(i).GetComponent<Image>());
            this.transform.GetChild(0).GetChild(i).GetComponent<Image>().color = Color.white;
        }

    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            LayerMask layerMask = LayerMask.GetMask("Tower");
            //只检测Tower层的射线
            if (Physics.Raycast(ray,out _raycastHit,100,layerMask) && currentChioceTowrtID != -1) {
                Transform tower = _raycastHit.transform;
                if (tower.childCount == 0) {//还没有放置炮塔
                    GameObject tow = Instantiate(towerPrefab[currentChioceTowrtID], Vector3.zero, 
                        Quaternion.identity);
                    tow.transform.parent = tower.transform;
                    tow.transform.localPosition = Vector3.up * 2.7f;
                }
            }
        }
    }
    
    /// <summary>
    /// Button的监听
    /// </summary>
    /// <param name="towerID"></param>
    public void TowerButtonOnClick(int towerID) {
        for (int i = 0; i < towerButton.Count; i++) {
            towerButton[i].color  = Color.white;
        }

        if (towerID != currentChioceTowrtID) {
            towerButton[towerID].color  = Color.green;
            currentChioceTowrtID = towerID;
        } else {
            currentChioceTowrtID = -1;
        }
     
    }
    
    /// <summary>
    /// 游戏结果
    /// </summary>
    /// <param name="str"></param>
    public void showResultText(String str) {
        VictoryImage.SetActive(true);
        Time.timeScale = 0;
        VictoryImage.GetComponentInChildren<Text>().text = str;
    }

}
