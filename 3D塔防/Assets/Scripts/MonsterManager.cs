using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MonsterManager : MonoBehaviour
{
    
    
    [System.Serializable]
    public class MonsterWaveMessage {
        [Header("每波的时间间隔")]
        public float waveInterval = 1f;
        [Header("当前波怪物生成时间间隔")]
        public float monsterCreateInterval = 1f;
        [Header("当前波怪物数量")]
        public int monsterCount = 3;
        [Header("当前波怪物预设体")]
        public GameObject monsterPrefab;
        [Header("当前波怪物血量倍率")]
        public float monsterHPRate = 1;
        [Header("当前波怪物移动速度倍率")]
        public float monsterSpeedRate = 1;
    }
    
    [Header("怪物波数信息")]
    public MonsterWaveMessage[] _monsterWaveMessages;
    //当前波速
    private int currentWave = 0;
    //当前生成怪的数量
    private int currentWaveMonsterCount = 0;
    //当前波怪物生成时间间隔计时
    private float currentWaveTimer = 0;
    //波数间隔计时
    private float waveIntervalTimer = 0;
    
    //怪物总数
    public static int monsterCount = 0;

    void Start()
    {
        for (int i = 0; i < _monsterWaveMessages.Length; i++) {
            monsterCount += _monsterWaveMessages[i].monsterCount;
        }
    }

    void Update()
    {
        
        if (currentWave < _monsterWaveMessages.Length) {
            MonsterWaveMessage currentMonsterWaveMessages = _monsterWaveMessages[currentWave];
            if (waveIntervalTimer < currentMonsterWaveMessages.waveInterval && currentWaveMonsterCount<currentMonsterWaveMessages.monsterCount) {
                
                if (currentWaveTimer < currentMonsterWaveMessages.monsterCreateInterval) {
                    currentWaveTimer += Time.deltaTime;
                } else {
                    currentWaveTimer = 0;
                    currentWaveMonsterCount++;
                    CreateMonster(currentMonsterWaveMessages);
                }
                
            } else {
                waveIntervalTimer += Time.deltaTime;
            }

            if (waveIntervalTimer>currentMonsterWaveMessages.waveInterval) {//下一波
                currentWave++;
                waveIntervalTimer = 0;
                currentWaveTimer = 0;
                currentWaveMonsterCount = 0;
            }
            
        } 
    }
    
    /// <summary>
    /// 生成当前波怪物
    /// </summary>
    private void CreateMonster(MonsterWaveMessage currentMonsterWaveMessages) {
        GameObject monsterPrefab = currentMonsterWaveMessages.monsterPrefab;
        // Debug.Log(currentMonsterWaveMessages.monsterHPRate);
        // Debug.Log(monsterPrefab.GetComponent<MonsterController>().HP);
        GameObject monster = Instantiate(monsterPrefab, this.transform.position, Quaternion.identity);

        monster.GetComponent<MonsterController>().HP *= currentMonsterWaveMessages.monsterHPRate;
        monster.GetComponent<NavMeshAgent>().speed *= currentMonsterWaveMessages.monsterSpeedRate;
        monster.name = "Monster-" + currentWave + "-" + currentWaveMonsterCount;
    }

  
}
