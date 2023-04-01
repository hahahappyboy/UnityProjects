using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using UnityEngine.UI;

public class MVC_View : MonoBehaviour
{
    private Text goldText;
    private Button button;
    
    public void Init() {
        goldText = this.transform.Find("Text").GetComponent<Text>();
        button = this.transform.Find("Button").GetComponent<Button>();
    }

    public Button GetButton() {
        return button;
    }

    public void RefreshGoldText(JsonData d) {
        goldText.text = d["GoldCount"].ToString();
    }
}
