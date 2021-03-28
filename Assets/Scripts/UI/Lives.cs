using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Lives : MonoBehaviour
{
    private PlayerEntity player;

    public Image lifeActive;
    public Image lifeInactive;
    public Vector3 offset = new Vector3(24, 0, 0);

    private List<Image> activeLives = new List<Image>();
    private List<Image> inactiveLives = new List<Image>();

    private int lives;

    void Start()
    {
        player = PlayerEntity.instance;
        for (int i = 0; i < player.health.NumLives; i++)
        {
            inactiveLives.Add(Instantiate(lifeInactive, transform));
            inactiveLives[i].transform.position += offset * i;
        }
        for (int i = 0; i < player.health.NumLives; i++)
        {
            activeLives.Add(Instantiate(lifeActive, transform));
            activeLives[i].transform.position += offset * i;
        }

        lifeActive.enabled = false;
        lifeInactive.enabled = false;

        lives = player.health.NumLives;
    }


    void Update()
    {
        if (lives != player.health.CurrentLives)
        {
            lives = player.health.CurrentLives;
            
            for (int i = 0; i < player.health.NumLives; i++)
            {
                activeLives[i].enabled = i < lives;
            }
        }
    }
}
