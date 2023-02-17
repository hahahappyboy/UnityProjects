using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour {
    //导航组件
    private NavMeshAgent _navMeshAgent;
    //导航终点
    private Vector3 _navEndPoint;
    //可以攻击自己的它
    private List<TowerController> _towerControllerList;

    private Animation _animation;
    private CapsuleCollider _capsuleCollider;
    private Rigidbody _rigidbody;

    public enum LiveState {
        Live,
        Die,
    }

    public LiveState liveState = LiveState.Live;
    
    [Header("基础血量")]
    public float HP = 10f;
    void Start() {
        _navMeshAgent.destination = _navEndPoint;
    }
    
 
    private void Awake() {
        _rigidbody = this.GetComponent<Rigidbody>();
        _capsuleCollider = this.GetComponent<CapsuleCollider>();
        _towerControllerList = new List<TowerController>();
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        _navEndPoint = GameObject.FindWithTag("End").transform.position;
        _animation = this.GetComponent<Animation>();
    }

    public TowerController AddTowerController(TowerController towerController) {
        if (!_towerControllerList.Contains(towerController)) {
            _towerControllerList.Add(towerController);
        }
        return towerController;
    }
    
    public TowerController RemoveTowerController(TowerController towerController) {
        if (_towerControllerList.Contains(towerController)) {
            _towerControllerList.Remove(towerController);
        }
        return towerController;
    }
    
    

    void Update() {
        if (this.liveState == LiveState.Die) {
            return;
        }
        currentAnimation();
    }
    /// <summary>
    /// 当前正在播放的动画
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void currentAnimation() {
        //收到伤害
        if (_animation.IsPlaying("Damage")) {
            Damage();
            return;
        }

        if (_animation.IsPlaying("Dead")) {
            
            Die();
            
            return;
        }
        
        
        
        if (!_animation.IsPlaying("Run")) {
            Run();
        }
        


    }

    private void Run() {
        _animation.CrossFade("Run");
        _navMeshAgent.enabled = true;
        _navMeshAgent.isStopped = false;
        _navMeshAgent.destination = _navEndPoint;
      
    }

    private void Damage() {
        // _navMeshAgent.isStopped = true;
        _navMeshAgent.enabled = false;
        
    }

    private void Die() {
        this.liveState = LiveState.Die;
        //关闭导航碰撞和碰撞体和刚体
        Destroy(_rigidbody);
        // _navMeshAgent.isStopped = true;
        _navMeshAgent.enabled = false;
        _capsuleCollider.enabled = false;
        
        //通知Tower将自己移除
        TowerRemoveMe();
        
        _towerControllerList.Clear();
    }

    public void TowerRemoveMe() {
        foreach (var towerController in _towerControllerList) {
            towerController.RemoveMonster(this);
        }
        MonsterManager.monsterCount--;
        if (MonsterManager.monsterCount<=0) {
            ButtonController.GetInstance().showResultText("成功");
        }
    }

    //收到伤害
    public void GetDamage(float damage) {
        HP -= damage;
        if (HP<= 0) { //死亡
            _animation.CrossFade("Dead");
        }else{ //收到伤害
           
            _animation.CrossFade("Damage");
        }
        
    }
}
