using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallBorder : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            //Kills Player
            var player = collider.gameObject.GetComponent<PlayerEntity>();
            player.health.Instakill();
        }
    }
}
