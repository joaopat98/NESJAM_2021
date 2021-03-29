using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDivider : RoomDivider
{
    [SerializeField] GameObject Boss;
    [SerializeField] GameObject Door;
    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        if (!Activated && collider.CompareTag("Player"))
        {
            RoomManager.ActivateDividerAndPrevious(this);
            Door.SetActive(true);
            Boss.SetActive(true);
        }
    }
}
