using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseEquipment : MonoBehaviour
{
    //
    public enum EquipmentSpecies{
        Cap,
        Cloth,
        Wing
    }

    public enum EquipmentState {
        BagBoxing,
        EquipBoxing,
    }
    
    /// <summary>
    /// 回到以前的位置
    /// </summary>
    public abstract void BackToOriginalPosition();
    /// <summary>
    /// 接受装备
    /// </summary>
    /// <param name="equipment"></param>
    public abstract void ReceiveEquipment(GameObject equipment);

}
