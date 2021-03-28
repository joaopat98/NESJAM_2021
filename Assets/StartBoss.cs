using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBoss : MonoBehaviour
{
    [SerializeField] GameObject Boss;
    [SerializeField] GameObject Door;
    bool started;
    void OnTriggerEnter2d(Collider2D collider)
    {
        if (!started && collider.CompareTag("Player"))
        {
            started = true;
            Door.SetActive(true);
            Boss.SetActive(true);
        }
    }
}
