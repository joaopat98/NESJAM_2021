using System.Collections;
using UnityEngine;

public abstract class BossSecondPhaseLightState : BossState
{
    protected Boss.Second phaseProps;
    protected Boss.Second.Light props;
    public static new T Create<T>(Boss target) where T : BossSecondPhaseLightState
    {
        var state = BossState.Create<T>(target);
        state.phaseProps = target.second;
        state.props = target.second.light;
        return state;
    }

    public override void OnGetHit()
    {
        base.OnGetHit();
        if (target.CurrentHealth <= phaseProps.HealthThreshold)
        {
            phaseProps.Assets.SetActive(false);
            SetState(BossThirdPhaseIdle.Create(target));
        }
    }

    protected override void WorldSwitch(WorldType world)
    {
        if (world == WorldType.Dark)
        {
            props.started = false;
            SetState(BossSecondPhaseDarkHover.Create(target));
        }
    }
}