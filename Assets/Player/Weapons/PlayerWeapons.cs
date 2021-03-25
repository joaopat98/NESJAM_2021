using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{

    private WeaponSet[] weaponSets;

    //arsenal and Weapons as circular lists to easily switchThrough
    private int currentIndex = 0;
    PlayerEntity player;

    void Awake()
    {
        weaponSets = GetComponentsInChildren<WeaponSet>();
        ActivateSet(currentIndex);
    }

    void Start()
    {
        player = GetComponent<PlayerEntity>();
        foreach (var set in weaponSets)
        {
            set.Init(player);
        }
    }

    public void SwitchToSet(int index)
    {
        currentIndex = index;
        ActivateSet(currentIndex);
    }

    public void SwitchToSet(string id)
    {
        int index = 0;
        while (weaponSets[index].id != id && index < weaponSets.Length) index++;
        if (index >= weaponSets.Length)
        {
            Debug.LogError("Invalid Weapon Set Id given!");
        }
        else
        {
            currentIndex = index;
            ActivateSet(currentIndex);
        }
    }

    private void ActivateSet(int index)
    {
        for (int i = 0; i < weaponSets.Length; i++)
        {
            weaponSets[i].gameObject.SetActive(i == index);
        }
    }



    #region "Public Getters"
    public WeaponSet GetCurrentSet() { return weaponSets[currentIndex]; }

    public string GetCurrentSetId() { return weaponSets[currentIndex].id; }
    #endregion

}