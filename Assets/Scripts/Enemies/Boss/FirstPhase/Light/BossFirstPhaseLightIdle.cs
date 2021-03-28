using UnityEngine;

public class BossFirstPhaseLightIdle : BossFirstPhaseLightState
{
    float t = 0;
    public static BossFirstPhaseLightIdle Create(Boss target)
    {
        var state = BossFirstPhaseLightState.Create<BossFirstPhaseLightIdle>(target);
        return state;
    }

    public override void StateStart()
    {
        base.StateStart();
        transform.position = props.Teleports[0].position;
    }

    public override void StateUpdate()
    {
        t += Time.deltaTime;
        if (t > props.Cooldown)
        {
            SetState(BossFirstPhaseLightThrow.Create(target));
        }
    }
}