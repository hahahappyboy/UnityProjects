using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentController : BaseEquipment, 
    IBeginDragHandler,IEndDragHandler,IDragHandler
{
    [Header("装备所处状态")]
    public EquipmentState equipmentState;

    [Header("装备种类")] 
    public EquipmentSpecies equipmentSpecies;
    
    //Image组件
    private Image equipmentImageGetComponent;
    //Canvas位置组件
    private Transform canvasTransform;
    //当前的parent对象，用于放回以前的位置
    private Transform beginDragParentTransform;
    
    private void Awake() {
        canvasTransform = GameObject.FindWithTag("Canvas").transform;
        equipmentImageGetComponent = this.GetComponent<Image>();
    }
    
    public void OnBeginDrag(PointerEventData eventData) {
        //关闭射线
        equipmentImageGetComponent.raycastTarget = false;
        //拖动前的位置
        beginDragParentTransform = this.transform.parent;
        //更改变父对象，让其能显示在最上层
        this.transform.SetParent(canvasTransform);
    }
    public void OnDrag(PointerEventData eventData) {
        this.transform.position = Input.mousePosition;
    }
    public void OnEndDrag(PointerEventData eventData) {
        GameObject eventDataGameObject = eventData.pointerEnter;
        //放入空的装备栏 或则 空的背包栏 或则 已经装备了的背包栏或装备栏
        if ((eventDataGameObject.tag == "EquipBox"||
            eventDataGameObject.tag == "BagBox"||
            eventDataGameObject.tag == "Equipment") &&
            eventDataGameObject.transform != beginDragParentTransform
            ) {
            if (eventDataGameObject.tag == "Equipment") {//已经装备了的背包栏或装备栏
                eventDataGameObject.GetComponent<EquipmentController>().ReceiveEquipment(this.gameObject);
            } else {//空的装备栏 或则 空的背包栏
                eventDataGameObject.GetComponent<BaseBox>().ReceiveEquipment(this.gameObject);
            }
        } else {
            BackToOriginalPosition();
        }
        equipmentImageGetComponent.raycastTarget = true;
    }
    /// <summary>
    /// 回到以前的位置
    /// </summary>
    public override void BackToOriginalPosition() {
        this.transform.position = beginDragParentTransform.position;
        this.transform.SetParent(beginDragParentTransform);
    }
    /// <summary>
    /// 接受装备
    /// </summary>
    /// <param name="equipment">要接受的装备</param>
    public override void ReceiveEquipment(GameObject equipment) {
        //拖动的装备
        EquipmentController equipmentController = equipment.GetComponent<EquipmentController>();
        //如果交换的其中一个在装备栏里->要判断类型相同不->相同交换，不相同返回原位
        if (this.equipmentState == EquipmentState.EquipBoxing ||
            equipmentController.equipmentState == EquipmentState.EquipBoxing
           ) {
            if ( this.equipmentSpecies == equipmentController.equipmentSpecies) {//类型相同
                //想要交换的装备所在Box
                BaseBox exchangeBox = this.transform.parent.GetComponent<BaseBox>();
                //拖动的装备所在Box
                BaseBox currentBox = equipment.GetComponent<EquipmentController>().beginDragParentTransform.GetComponent<BaseBox>();
                currentBox.ReceiveEquipment(this.gameObject);
                exchangeBox.ReceiveEquipment(equipmentController.gameObject);

            } else {//类型不同
                equipmentController.BackToOriginalPosition();
            }
            
        } else { //两个背包里的物品交换
            //想要交换的装备所在Box
            BaseBox exchangeBox = this.transform.parent.GetComponent<BaseBox>();
            //拖动的装备所在Box
            BaseBox currentBox = equipment.GetComponent<EquipmentController>().beginDragParentTransform.GetComponent<BaseBox>();
            currentBox.ReceiveEquipment(this.gameObject);
            exchangeBox.ReceiveEquipment(equipmentController.gameObject);
        }
    }
}
