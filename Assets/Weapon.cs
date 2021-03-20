using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Weapon : MonoBehaviour
{
    public bool Unlocked;

    protected virtual void OnEnable() {}

    protected virtual void OnDisable() {}

    protected abstract void OnFire(InputValue value);
}
