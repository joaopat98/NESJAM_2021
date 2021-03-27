using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpecifier : MonoBehaviour
{
    [SerializeField] WorldType activeType;

    public void OnWorldSwitch(WorldType newWorld)
    {
        if (newWorld == activeType)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
