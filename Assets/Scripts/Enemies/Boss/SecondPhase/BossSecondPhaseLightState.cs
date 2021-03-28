using System.Collections;
using UnityEngine;

public abstract class BossSecondPhaseLightState : BossState
{
    protected Boss.Light.Second props;
    public static new T Create<T>(Boss target) where T : BossSecondPhaseLightState
    {
        var state = BossState.Create<T>(target);
        state.props = target.light.second;
        return state;
    }

    public override void OnGetHit()
    {
        base.OnGetHit();
        if (target.CurrentHealth <= props.HealthThreshold)
        {
            props.Assets.SetActive(false);
            SetState(BossThirdPhaseIdle.Create(target));
        }
    }
}