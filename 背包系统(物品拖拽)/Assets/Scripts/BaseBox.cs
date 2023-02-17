using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBox : MonoBehaviour
{
    
    //能接受装备的类型
    public enum CanReceiveEquipment {
        All,
        Cap,
        Cloth,
        Wing,
        Other
    }
    //接受装备 
    public abstract void ReceiveEquipment(GameObject equipment);
}
