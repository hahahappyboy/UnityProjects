
using System;
using UnityEngine;


[RequireComponent(typeof(CanvasGroup))]
public class UIModuleBase : MonoBehaviour {
    private CanvasGroup _canvasGroup;
    
    public void Awake() {
        _canvasGroup = this.GetComponent<CanvasGroup>();
    }

    //第一次加载该界面，显示在最上面时
    public virtual void UIEnter() {
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.alpha = 1;
    }

    //当前界面被其他界面遮挡时
    public virtual void UIPause() {
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.alpha = 0.5f;
    }

    //其他界面退出，该页面处于最上面时
    public virtual void UIResume() {
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.alpha = 1;
    }
    
    public virtual void UIExit() {
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.alpha = 0;
    }
    
}