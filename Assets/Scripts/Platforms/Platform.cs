using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    protected bool active;

    public void Activate()
    {
        active = true;
    }

    public void Deactivate()
    {
        active = false;
    }

    // Start is called before the first frame update
    protected virtual void Start() { }

}
