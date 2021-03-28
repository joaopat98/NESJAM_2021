using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    private PlayerEntity player;

    void Start()
    {
        player = PlayerEntity.instance;
    }

    void Update()
    {
        slider.value = (float) player.health.CurrentHealth / player.health.MaxHealth;
    }
}
