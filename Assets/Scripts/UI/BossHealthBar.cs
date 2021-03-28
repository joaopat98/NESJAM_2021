using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Slider slider;
    public Boss boss;

    void Update()
    {
        slider.value = (float)boss.CurrentHealth / boss.MaxHealth;
    }
}
