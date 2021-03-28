using System.Collections;
using UnityEngine;

public abstract class BossFirstPhaseDarkState : BossState
{
    protected Boss.First phaseProps;
    protected Boss.First.Dark props;
    public static new T Create<T>(Boss target) where T : BossFirstPhaseDarkState
    {
        var state = BossState.Create<T>(target);
        state.phaseProps = target.first;
        state.props = target.first.dark;
        return state;
    }

    public override void OnGetHit()
    {
        base.OnGetHit();
        if (!target.isClone && target.CurrentHealth <= phaseProps.HealthThreshold)
        {
            phaseProps.Assets.SetActive(false);
            //SetState(BossSecondPhaseDarkThrow.Create(target));
        }
    }
}