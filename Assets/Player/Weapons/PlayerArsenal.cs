using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArsenal : MonoBehaviour
{
    [Serializable]
    struct WeaponSet{
        public string Name; //To be easily set directly from other scripts
        public Weaponry[] Weapons;
    }

    [Serializable]
    struct Weaponry{
        public string Name; //To be easily set directly from other scripts
        public GameObject Weapon;
    }

    [SerializeField]
    private WeaponSet[] arsenal;

    //arsenal and Weapons as circular lists to easily switchThrough
    private int currentSet = 0;
    private int currentWeapon = 0;

    //Just for abstraction
    private const int NOTFOUND = -1;

    #region "Set Current WeaponSet and Weapon"
    private void ResetSetAndWeapon(){
        ResetCurrentSet();
        ResetCurrentWeapon();
    }

    private int ResetCurrentSet(){ return SetCurrentSet(0); }
    private int ResetCurrentWeapon(){ return SetCurrentWeapon(0); }

    private int SetCurrentSet(int index){ 
        currentSet = index;
        return currentSet;
    }

    private int SetCurrentWeapon(int index){
        currentWeapon = index;
        return currentWeapon;
    }
    #endregion

    #region "Get Index"

    private int GetWeaponSetIndex(string name){
        for (int i = 0; i < arsenal.Length; i++){
            if (arsenal[i].Name == name){ return i; }
        }
        return NOTFOUND;
    }

    private int GetWeaponIndex(string name){
        for (int i = 0; i < arsenal[currentSet].Weapons.Length; i++){
            if (arsenal[currentSet].Weapons[i].Name == name){ return i; }
        }
        return NOTFOUND;
    }

    #endregion

    #region "Circular List Functions (SetNext)"
    public void SetNextWeaponSet(){
        if (currentSet + 1 < arsenal.Length){
            SetCurrentSet(currentSet + 1);
        } else { ResetCurrentSet(); }

        ResetCurrentWeapon();
    }

    public void SetNextWeapon(){
        if (currentWeapon + 1 < arsenal[currentSet].Weapons.Length){
            SetCurrentWeapon(currentWeapon + 1);
        } else { ResetCurrentWeapon(); }
    }
    #endregion

    #region "Set WeaponSet or Weapon Based on argument"

    public void SwitchToWeaponSet(string name){
        int index = GetWeaponSetIndex(name);
        if (index != NOTFOUND){
            SetCurrentSet(index);
            ResetCurrentWeapon();
        }
    }

    public void SwitchToWeapon(string name){
        int index = GetWeaponIndex(name);
        if (index != NOTFOUND){
            SetCurrentWeapon(index);
        } 
    }

    #endregion

    #region "Public Getters"
    public GameObject GetCurrentWeapon(){
        return arsenal[currentSet].Weapons[currentWeapon].Weapon;
    }

    public string GetCurrentSetName(){
        return arsenal[currentSet].Name;
    }

    public string GetCurrentWeaponName(){
        return arsenal[currentSet].Weapons[currentWeapon].Name;
    }

    #endregion

}