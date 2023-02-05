using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIManager.GetInstance().PushUI("MainModule");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UIManager.GetInstance().PushUI("BagModule");
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            UIManager.GetInstance().PushUI("EquipMsgModule");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UIManager.GetInstance().PopUI();
        }
    }
}
