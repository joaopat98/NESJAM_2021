using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerEntity player = collision.collider.GetComponent<PlayerEntity>();
            player.health.Hit(player.health.Health);
            /* if (Vector2.Angle(Vector2.up, normal) < 10f)
                collision.transform.parent = transform; */
        }
    }
}
