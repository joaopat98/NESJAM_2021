using UnityEngine;

public class FlyerFlying : FlyerState
{
    public static FlyerFlying Create(Flyer target)
    {
        FlyerFlying state = FlyerState.Create<FlyerFlying>(target);
        return state;
    }

    public override void StateStart()
    {
        base.StateStart();
    }

    public override void StateUpdate()
    {
        //TODO voar em onda
        if ((this.transform.position - target.startPosition).x >= target.flightRange)
        {
            SetState(FlyerTeleportOut.Create(target));
        }
    }
}
