using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : Platform
{
    protected override void Start()
    {
        base.Start();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerLadderMovement>().EnterLadder();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerLadderMovement>().ExitLadder();
        }
    }
}
