using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using UnityEngine.UI;

public class MVC_Controller : MonoBehaviour {
    
    private Button button;
    private MVC_View view;
    void Awake() {
        view = GetComponent<MVC_View>();
        view.Init();

        button = view.GetButton();
        button.onClick.AddListener(IconAdd);
        
        JsonData d = MVC_Model.ReadJsonData();
        view.RefreshGoldText(d);
    }
    
    public void IconAdd() {
        JsonData d = MVC_Model.UpdateJsonData();
        view.RefreshGoldText(d);
    }
}

