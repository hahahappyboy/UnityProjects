using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OKButtonController : MonoBehaviour {
    private Transform selectFrameTransformer;

    private void Awake() {
        selectFrameTransformer = GameObject.FindWithTag("SelectFrame").transform;
    }

    public void OnButtonOKClick() {
        Dictionary<string, object> param = new Dictionary<string, object>();
        param.Add("SelectNum", selectFrameTransformer.GetComponent<SelectFrameController>().SelectNum);
	    SceneDataManager.GetInstance().ToNewScene("LevelScene", param);
    }
 
}
