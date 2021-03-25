using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSet : MonoBehaviour
{
    Weapon[] weapons;
    public string id;
    int currentIndex = 0;
    protected PlayerEntity player;

    void Awake()
    {
        weapons = GetComponentsInChildren<Weapon>();
        ActivateWeapon(currentIndex);
    }

    public virtual void Init(PlayerEntity player)
    {
        this.player = player;
        foreach (var weapon in weapons)
        {
            weapon.Init(player);
        }
    }

    public void NextWeapon()
    {
        do
        {
            currentIndex = (currentIndex + 1) % weapons.Length;
        } while (!weapons[currentIndex].Unlocked);
        ActivateWeapon(currentIndex);
    }

    void ActivateWeapon(int index)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(i == index);
        }
    }

    protected virtual void OnEnable()
    {
        currentIndex = 0;
        ActivateWeapon(currentIndex);
    }

    // Update is called once per frame
    void Update()
    {

    }

    #region "Public Getter"
    public Weapon GetCurrentWeapon() { return weapons[currentIndex]; }
    #endregion
}
