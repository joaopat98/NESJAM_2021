using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Weapon : MonoBehaviour
{
    public bool Unlocked;
    protected PlayerEntity player;

    public virtual void Init(PlayerEntity player)
    {
        this.player = player;
    }

    protected virtual void OnEnable() { }

    protected virtual void OnDisable() { }

    protected abstract void OnFire(InputValue value);
}
