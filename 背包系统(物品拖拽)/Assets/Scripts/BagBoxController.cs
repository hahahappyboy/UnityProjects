using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagBoxController : BaseBox
{
    [Header("能装备的类型")]
    public CanReceiveEquipment canReceiveEquipment = CanReceiveEquipment.All;
    
    public override void ReceiveEquipment(GameObject equipment) {
        // Debug.Log(this);
        equipment.transform.SetParent(this.transform);
        equipment.transform.localPosition = Vector3.zero;
        equipment.GetComponent<EquipmentController>().equipmentState = BaseEquipment.EquipmentState.BagBoxing;
    }
}
