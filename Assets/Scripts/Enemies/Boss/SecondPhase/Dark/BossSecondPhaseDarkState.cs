using System.Collections;
using UnityEngine;

public abstract class BossSecondPhaseDarkState : BossState
{
    protected Boss.Second phaseProps;
    protected Boss.Second.Dark props;
    public static new T Create<T>(Boss target) where T : BossSecondPhaseDarkState
    {
        var state = BossState.Create<T>(target);
        state.phaseProps = target.second;
        state.props = target.second.dark;
        return state;
    }

    public override void OnGetHit()
    {
        base.OnGetHit();
        if (!target.isClone && target.CurrentHealth <= phaseProps.HealthThreshold)
        {
            phaseProps.Assets.SetActive(false);
            target.SecondShield.SetActive(false);
            SetState(BossThirdPhaseIdle.Create(target));
        }
    }

    protected override void WorldSwitch(WorldType world)
    {
        if (world == WorldType.Light)
        {
            target.SecondShield.SetActive(false);
            props.started = false;
            SetState(BossSecondPhaseLightThrow.Create(target));
        }
    }
}