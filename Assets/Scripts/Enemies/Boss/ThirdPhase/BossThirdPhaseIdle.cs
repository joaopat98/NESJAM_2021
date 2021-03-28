using UnityEngine;

public class BossThirdPhaseIdle : BossThirdPhaseState
{
    float t = 0;
    public static BossThirdPhaseIdle Create(Boss target)
    {
        var state = BossThirdPhaseState.Create<BossThirdPhaseIdle>(target);
        return state;
    }

    public override void StateStart()
    {
        base.StateStart();
        if (!props.Started)
        {
            props.Started = true;
            props.Assets.SetActive(true);
            TeleportToNext();
        }
    }

    public override void StateUpdate()
    {
        t += Time.deltaTime;
        if (t > props.TimeBeforeShot)
        {
            SetState(BossThirdPhaseShoot.Create(target));
        }
    }
}