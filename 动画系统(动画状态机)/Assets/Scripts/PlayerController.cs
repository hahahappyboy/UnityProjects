using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour {
    
    //相机
    [Header("相机")]
    public Transform cameraTransform;
    
    private float virAxis;
    private float horAxis;
    //监听
    private bool isSneak;
    private bool isShout;
    
    //参数
    private int runSpeedParameter;
    private int isSneakParameter;
    private int isShoutParameter;

    //角色属性
    private const float MOVE_MAX_SPEED = 5.6f;
    [Header("旋转速度")]
    public float turnSpeed = 3f;
    private Animator _animator;
    //移动的方向
    private Vector3 moveDir;
    private Quaternion moveQua;
    
    //相机相对于玩家最初的方向向量
    private Vector3 originalPlayer2Camera;
    //音效
    private AudioSource _audioSource;
    [Header("走路音效")]
    public AudioClip stepAudioClip;
    [Header("呼喊音效")]
    public AudioClip shoutAudioClip;

    
    private void Awake() {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        originalPlayer2Camera = cameraTransform.position - this.transform.position;
    }

    void Update() {
        virAxis = Input.GetAxis("Vertical");
        horAxis = Input.GetAxis("Horizontal");
        isSneak = Input.GetButton("Sneak");
        isShout = Input.GetButtonDown("Shout");

        runSpeedParameter = Animator.StringToHash("RunSpeed");
        isSneakParameter = Animator.StringToHash("IsSneak");
        isShoutParameter = Animator.StringToHash("Shout");

        if (virAxis != 0 || horAxis!= 0) {
            //播放动画            
            _animator.SetFloat(runSpeedParameter,MOVE_MAX_SPEED,0.3f,Time.deltaTime);
            //获取移动方向
            moveDir = new Vector3(horAxis, 0, virAxis);
            //将方向转化为四元数
            moveQua = Quaternion.LookRotation(moveDir);
            //角色转身
            transform.rotation = Quaternion.Lerp(transform.rotation, moveQua, Time.deltaTime * turnSpeed);
        } else {
            _animator.SetFloat(runSpeedParameter,0,0.1f,Time.deltaTime);
        }

        if (isSneak) {
            _animator.SetBool(isSneakParameter,true);
        } else {
            _animator.SetBool(isSneakParameter,false);
        }

        if (isShout &&
            !_animator.GetCurrentAnimatorStateInfo(1).IsName("Shout")&&
            !_animator.IsInTransition(1)) {
            _animator.SetTrigger(isShoutParameter);
            AudioSource.PlayClipAtPoint(shoutAudioClip,this.transform.position);
        }
        //音效
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Run")) {
            if (!_audioSource.isPlaying) {
                _audioSource.clip = stepAudioClip;
                _audioSource.Play();
            }
        } else {
            _audioSource.Pause();
        }

        
        CanmeraFollow();

    }

    private void CanmeraFollow() {
        Vector3 followDir =cameraTransform.position - this.transform.position;
        Vector3 moveDir = originalPlayer2Camera - followDir;
        float moveSpeed = 3f;
        cameraTransform.position =
            Vector3.Lerp(cameraTransform.position, moveDir + cameraTransform.position, Time.deltaTime * moveSpeed);
    }
}
