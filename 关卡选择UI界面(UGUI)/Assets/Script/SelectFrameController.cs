using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectFrameController : MonoBehaviour {
    //当前选的是哪一个关卡
    private int selectNum = 0;
    
    public int SelectNum {
        set { selectNum = value; }
        get { return selectNum; }
    }
}
