using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour {
    
    private List<MonsterController> _monsterList;
    [Header("炮弹")]
    public GameObject bullet;
    
    [Header("开火时间间隔")]
    public float fireTimerInterval = 2f;

    private float fireTimer;
    
    //开火口
    private Transform firePointTransform;
    //炮身
    private Transform turretTransform;
    [Header("炮口旋转速度")]
    public float turnSmoothSpeed = 3f;
    private void Awake() {
        _monsterList = new List<MonsterController>();
        firePointTransform = this.transform.Find("Base/Turret/Barrel/FirePoint");
        turretTransform = this.transform.Find("Base/Turret");
    }
    
    void Start() {
        fireTimer = fireTimerInterval;
    }

    void Update() {
        fireTimer += Time.deltaTime;
        
        if (_monsterList.Count != 0) {//有怪物
            MonsterController monster = _monsterList[0].GetComponent<MonsterController>();
            //转向mosnter
            float angel = turn2Moster(monster);
            // Debug.Log(angel);
            if (fireTimer >= fireTimerInterval && angel<15f) {
                fireMonster(monster);
                fireTimer = 0;
            }
        }

       

    }

    private void fireMonster(MonsterController monster) {
        GameObject bulletGameObject = Instantiate(bullet, turretTransform.position, Quaternion.identity);
        BulletController bulletController = bulletGameObject.GetComponent<BulletController>();
        // bulletGameObject.transform.parent = turretTransform;
        // bulletGameObject.transform.localRotation = Quaternion.Euler((new Vector3(-90,0,0)));
        bulletController.SetAttackMonster(monster);
        
    }

   /// <summary>
   /// 转向Monster
   /// </summary>
   /// <param name="monster"></param>
   /// <returns>返回两者相差角度</returns>
    private float turn2Moster(MonsterController monster) {
        Vector3 i2monster = monster.transform.position-turretTransform.position + Vector3.up * 1f + Vector3.forward * 0.5f;
        Quaternion targetRoate = Quaternion.LookRotation(i2monster);
        turretTransform.rotation = Quaternion.Lerp(turretTransform.rotation,targetRoate,turnSmoothSpeed * Time.deltaTime);
        return Vector3.Angle(turretTransform.forward, i2monster);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Monster") {
            MonsterController monster = other.GetComponent<MonsterController>();
            if (!_monsterList.Contains(monster)) {
                //添加攻击目标
                _monsterList.Add(monster);
                //Monster添加攻击炮塔
                monster.AddTowerController(this);
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Monster") {
            MonsterController monster = other.GetComponent<MonsterController>();
            if (_monsterList.Contains(monster)) {
                _monsterList.Remove(monster);
                //移除被锁定的炮塔
                monster.RemoveTowerController(this);
            }
        }
    }

    
    public void RemoveMonster(MonsterController monsterController) {
        if (_monsterList.Contains(monsterController)) {
            _monsterList.Remove(monsterController);
        }
    }
}
