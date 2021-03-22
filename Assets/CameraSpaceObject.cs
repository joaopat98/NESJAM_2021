using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSpaceObject : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("MainCamera"))
        {
            Destroy(gameObject);
        }
    }
}
