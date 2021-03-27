using UnityEngine;

public class FlyerTeleportIn : FlyerState
{
    public static FlyerTeleportIn Create(Flyer target)
    {
        FlyerTeleportIn state = FlyerState.Create<FlyerTeleportIn>(target);
        return state;
    }

    public override void StateStart()
    {
        base.StateStart();
        transform.position = StartPosition;
        animator.Play("TeleportIn");
        SetState(FlyerFlying.Create(target));
    }

    public override void StateUpdate()
    {
        //só toca a animação nova e transita de estado
    }
}
