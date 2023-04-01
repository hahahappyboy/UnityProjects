using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;
using UnityEngine.UI;

public class NoMVC : MonoBehaviour {
    //Application.persistentDataPath:   Unity可写目录在C盘的 
    
    private Text goldText;
    private Button button;
    void Awake() {
        Debug.Log(Config.SAVEJSON_PATH);
        
        goldText = this.transform.Find("Text").GetComponent<Text>();
        button = this.transform.Find("Button").GetComponent<Button>();
        button.onClick.AddListener(IconAdd);
        //获取Json文件
        if (!File.Exists(Config.SAVEJSON_PATH)) {//Json文件不存在
            JsonData d = new JsonData();
            d["GoldCount"] = 0;
            File.WriteAllText(Config.SAVEJSON_PATH,d.ToJson());
        } else {//Json文件存在
            string json = File.ReadAllText(Config.SAVEJSON_PATH);
            JsonData d = JsonMapper.ToObject(json);
            goldText.text = d["GoldCount"].ToString();
         }
    }

    public void IconAdd() {
        //读取数据
        string json = File.ReadAllText(Config.SAVEJSON_PATH);
        JsonData d = JsonMapper.ToObject(json);
        //金币数+1
        d["GoldCount"] = (int)d["GoldCount"] + 1;
        
        //显示新的金币数
        goldText.text = d["GoldCount"].ToString();
        //更新保存新的Json数据
        File.WriteAllText(Config.SAVEJSON_PATH,d.ToJson());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
