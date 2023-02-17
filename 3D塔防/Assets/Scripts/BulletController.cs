using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BulletController : MonoBehaviour {
    private Transform monster;
    [Header("炮弹移动速度")]
    public float moveSmoothSpeed = 3f;

    [Header("攻击力")]
    public float damage = 4;

    public Transform SetAttackMonster(MonsterController monsterController) {
        monster = monsterController.transform;
        return monster;
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        AttackMonster();

    }

    private void AttackMonster() {
        MonsterController monsterController = monster.GetComponent<MonsterController>();

        float distance = FollowMonster();
        
        //命中敌人,并且此时敌人没死
        if (distance < 0.5f ) {
            //不在跟踪怪物
            Destroy(this);
            if ( monsterController.liveState == MonsterController.LiveState.Live) {
                monsterController.GetDamage(this.damage);
            }
        }
        
    }

  
    
    private float FollowMonster() {
        this.transform.position = Vector3.Lerp(this.transform.position,monster.position,Time.deltaTime * moveSmoothSpeed);
        return Vector3.Distance(this.transform.position, monster.position);
    }
}
