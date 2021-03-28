using System.Collections;
using UnityEngine;

public abstract class BossThirdPhaseState : BossState
{
    protected Boss.Third props;
    protected Coroutine teleportCoroutine;
    public static new T Create<T>(Boss target) where T : BossThirdPhaseState
    {
        var state = BossState.Create<T>(target);
        state.props = target.third;
        return state;
    }

    protected void TeleportToNext()
    {
        int newTeleport;
        do
        {
            newTeleport = Random.Range(0, props.Teleports.Length);
        } while (newTeleport == props.currentTeleport);
        props.currentTeleport = newTeleport;
        transform.position = props.Teleports[props.currentTeleport].position;
    }

    public override void OnGetHit()
    {
        base.OnGetHit();

    }
}