using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class RPGShopDataBase : DatasetBase {
    private HeroInfo _currentHeroInfo;
    private List<Equip> _equips;

    public RPGShopDataBase(string dataPath,string name) {
        _currentHeroInfo = new HeroInfo();
        // Debug.Log(dataPath);
        OpenDataset(dataPath);
        InitHero(name);
        InitEquipInfo();
    }
        
    ~RPGShopDataBase() {
        CloseDataset();
    }

    #region Hero

    private void InitHero(string name) {
        string sql = "Select * from HeroInfo Where Name= '" + name+"'";
        Dictionary<string, string> dictionary = ExecuteReaderSQL(sql)[0];
        // foreach (var VARIABLE in dictionary.Keys) {
        //     Debug.Log(VARIABLE);
        // }
        if (dictionary.Count != 0) {
            _currentHeroInfo.Name = dictionary["Name"];
            _currentHeroInfo.Money = Int32.Parse(dictionary["Money"]);
            _currentHeroInfo.HP = Int32.Parse(dictionary["HP"]);
            _currentHeroInfo.AD = Int32.Parse(dictionary["AD"]);
            _currentHeroInfo.Defense = Int32.Parse(dictionary["Defense"]);
            _currentHeroInfo.Knowing = Int32.Parse(dictionary["Knowing"]);
            // Debug.Log(_currentHeroInfo.Name);
            // Debug.Log(_currentHeroInfo.HP);
            // Debug.Log(_currentHeroInfo.AD);
            // Debug.Log(_currentHeroInfo.Defense);
            // Debug.Log(_currentHeroInfo.Knowing);
        } else {
            Debug.LogError("没有此英雄");
            return;
        }
        sql = "Select * from HeroEquip Where Name='" + name+"'";
        Dictionary<string, string> equip = ExecuteReaderSQL(sql)[0];
        if (equip.Count != 0) {
            _currentHeroInfo.Equips[0] = equip["Equip1"];
            _currentHeroInfo.Equips[1] = equip["Equip2"];
            _currentHeroInfo.Equips[2] = equip["Equip3"];
            _currentHeroInfo.Equips[3] = equip["Equip4"];
            _currentHeroInfo.Equips[4] = equip["Equip5"];
            _currentHeroInfo.Equips[5] = equip["Equip6"]; 
            // Debug.Log(_currentHeroInfo.Equips[0]);
            // Debug.Log(_currentHeroInfo.Equips[1]);
            // Debug.Log(_currentHeroInfo.Equips[2]);
            // Debug.Log(_currentHeroInfo.Equips[3]);
            // Debug.Log(_currentHeroInfo.Equips[4]);
            // Debug.Log(_currentHeroInfo.Equips[5]);
        }
    }

    public HeroInfo GetHero() {
        return _currentHeroInfo;
    }

    public void UpdateHeroInfo(HeroInfo hero) {
        string sql = "Update HeroInfo Set (Money,HP,AD,Defense,Knowing)=("+hero.Money+hero.HP+hero.AD+hero.Defense+hero.Knowing+") Where Name= '" + hero.Name+"'";
        ExecuteNonQuerySQL(sql);
    }
   
    #endregion

    #region 装备

    public void InitEquipInfo() {
        string sql = "Select * from EquipInfo";
        List < Dictionary<string, string> > list = ExecuteReaderSQL(sql);
        for (int i = 0; i < list.Count; i++) {
            Equip equip = new Equip();
            equip.EquipName = list[i]["EquipName"];
            equip.EquipMoney = Int32.Parse(list[i]["EquipMoney"]);
            equip.EquipHP = Int32.Parse(list[i]["EquipHP"]);
            equip.EquipAD = Int32.Parse(list[i]["EquipAD"]);
            equip.EquipDefense = Int32.Parse(list[i]["EquipDefense"]);
            equip.EquipKnowing = Int32.Parse(list[i]["EquipKnowing"]);
            _equips.Add(equip);
        }
    }

    public List<Equip> GetEquipInfo() {
        return _equips;
    }
    #endregion
    
}
