using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class RoomDivider : MonoBehaviour
{
    protected bool Activated;

    public Vector3 GetRespawnPoint()
    {
        return transform.position;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (!Activated && collider.CompareTag("Player"))
        {
            RoomManager.ActivateDividerAndPrevious(this);
        }
    }

    public void Activate()
    {
        Activated = true;
    }
}
