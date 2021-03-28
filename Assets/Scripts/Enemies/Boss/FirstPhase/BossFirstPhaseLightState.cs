using System.Collections;
using UnityEngine;

public abstract class BossFirstPhaseLightState : BossState
{
    protected Boss.Light.First props;
    protected Coroutine teleportCoroutine;
    public static new T Create<T>(Boss target) where T : BossFirstPhaseLightState
    {
        var state = BossState.Create<T>(target);
        state.props = target.light.first;
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
        StartCoroutine(PrepareTeleport());
    }
}