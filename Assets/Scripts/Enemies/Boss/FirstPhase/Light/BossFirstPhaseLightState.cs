using System.Collections;
using UnityEngine;

public abstract class BossFirstPhaseLightState : BossState
{
    protected Boss.First phaseProps;
    protected Boss.First.Light props;
    protected Coroutine teleportCoroutine;
    public static new T Create<T>(Boss target) where T : BossFirstPhaseLightState
    {
        var state = BossState.Create<T>(target);
        state.phaseProps = target.first;
        state.props = target.first.light;
        return state;
    }

    protected void TeleportToNext()
    {
        props.currentTeleport = (props.currentTeleport + 1) % props.Teleports.Length;
        transform.position = props.Teleports[props.currentTeleport].position;
    }

    protected IEnumerator PrepareTeleport()
    {
        if (teleportCoroutine != null)
            StopCoroutine(teleportCoroutine);
        yield return new WaitForSeconds(props.TeleportTime);
        TeleportToNext();
        teleportCoroutine = null;
    }

    public override void OnGetHit()
    {
        base.OnGetHit();
        if (target.CurrentHealth <= phaseProps.HealthThreshold)
        {
            if (teleportCoroutine != null)
                StopCoroutine(teleportCoroutine);
            phaseProps.Assets.SetActive(false);
            SetState(BossSecondPhaseLightThrow.Create(target));
        }
        else
        {
            StartCoroutine(PrepareTeleport());
        }
    }
}