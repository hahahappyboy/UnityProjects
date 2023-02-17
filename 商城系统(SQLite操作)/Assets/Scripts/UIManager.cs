using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region 获取UI组件

    private Texture heroNameTexture;
    private Transform heroPropertiesTextTransform;
    private Texture heroMoneyTexture;
    private Transform bagWindowTransform;
    private Transform shopWindowTransform;
    private void GetUIComponent() {
        heroNameTexture = this.transform.Find("HeroNameText").GetComponent<Texture>();
        heroPropertiesTextTransform = this.transform.Find("HeroPropertiesText").GetComponent<Transform>();
        heroMoneyTexture = this.transform.Find("MoneyIcon/Text").GetComponent<Texture>();
        bagWindowTransform = this.transform.Find("BagWindow").GetComponent<Transform>();
        shopWindowTransform = this.transform.Find("ShopWindow").GetComponent<Transform>();
    }
    #endregion

    #region 预制体
    private GameObject bagEquip;
    private GameObject shopEquip;
    private void GetGameObject() {
        bagEquip = Resources.Load<GameObject>("Prefabs/BagEquip");
        shopEquip = Resources.Load<GameObject>("Prefabs/ShopEquip");
    }
    #endregion

    #region DataBase

    private RPGShopDataBase _shopDataBase;
    private HeroInfo _currentHeroInfo;
    private List<Equip> _equips;
    private void GetDataBaseInfo() {
        string dataPath = "Data Source = " + Application.streamingAssetsPath + "/" + "UnitySQLite.db";
        string heroName = "比安卡深痕";
        _shopDataBase = new RPGShopDataBase(dataPath,heroName);
        _equips = _shopDataBase.GetEquipInfo();
    }
    private void InitShop() {
        for (int i = 0; i < _equips.Count; i++) {
            Debug.Log(_equips[i].EquipName);
        }
    }
    #endregion

    private void Awake() {
        GetDataBaseInfo();
        GetUIComponent();
        GetGameObject();    
    }
    void Start() {
        InitShop();
    }

    void Update()
    {
        
    }
}
