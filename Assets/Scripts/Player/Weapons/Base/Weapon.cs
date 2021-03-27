using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Weapon : MonoBehaviour
{
    public bool Unlocked;
    protected PlayerEntity player;

    public float TimeBetweenFire;
    protected float TimeSinceLastFire;
    private bool ableToFire = false;
    public virtual void Init(PlayerEntity player)
    {
        this.player = player;
    }

    protected virtual void OnEnable() { }

    protected virtual void OnDisable() { }

    protected abstract void OnFire(InputValue value);

    #region "Fire Rate"
    protected virtual bool GetReadyToFire()
    {
        if (TimeSinceLastFire >= TimeBetweenFire)
        {
            ableToFire = true;
        }
        else
        {
            ableToFire = false;
        }
        return TimeSinceLastFire >= TimeBetweenFire;
    }

    protected virtual bool GetAbleToFire()
    {
        return ableToFire;
    }

    protected virtual void ResetTimeSinceLastFire()
    {
        TimeSinceLastFire = 0;
    }

    protected virtual void AddTimeSinceLastFire(float value)
    {
        TimeSinceLastFire += value;
    }
    #endregion
}
