using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndController : MonoBehaviour {
    [Header("玩家生命值")] public int HP;
    
    void Start() {
        HP = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Monster") {
            HP--;
            GameObject moster = other.gameObject;
            moster.GetComponent<MonsterController>().TowerRemoveMe();
            Destroy(moster);
            
            if (HP <= 0) { //失败
                ButtonController.GetInstance().showResultText("失败");
            }

            if (MonsterManager.monsterCount == 0) {//怪物进入终点但 HP>0
                ButtonController.GetInstance().showResultText("成功");
            }
        }
    }
}
