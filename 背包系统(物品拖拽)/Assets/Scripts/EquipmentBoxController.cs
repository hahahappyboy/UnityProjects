using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentBoxController : BaseBox {
    [Header("能接受的装备类型")]
    public CanReceiveEquipment canReceiveEquipment;
    public override void ReceiveEquipment(GameObject equipment) {
        EquipmentController equipmentController = equipment.GetComponent<EquipmentController>();
        if (equipmentController.equipmentSpecies.ToString()==canReceiveEquipment.ToString()) {
            equipment.transform.SetParent(this.transform);
            equipment.transform.localPosition = Vector3.zero;
            equipmentController.equipmentState = BaseEquipment.EquipmentState.EquipBoxing;
        } else {
            equipmentController.BackToOriginalPosition();
        }
    }
}
