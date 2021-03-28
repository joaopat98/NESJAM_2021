using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    private PlayerEntity player;
    private Image image;
    private Weapon lastWeapon;

    void Start()
    {
        player = PlayerEntity.instance;
        image = gameObject.GetComponent<Image>();
        lastWeapon = player.weapons.GetCurrentSet().GetCurrentWeapon();
        image.sprite = lastWeapon.icon;
    }

    void Update()
    {
        if (lastWeapon != player.weapons.GetCurrentSet().GetCurrentWeapon())
        {
            lastWeapon = player.weapons.GetCurrentSet().GetCurrentWeapon();
            image.sprite = lastWeapon.icon;
        }
    }
}
