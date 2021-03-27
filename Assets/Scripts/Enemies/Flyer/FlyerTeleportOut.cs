using UnityEngine;

public class FlyerTeleportOut : FlyerState
{
    public static FlyerTeleportOut Create(Flyer target)
    {
        FlyerTeleportOut state = FlyerState.Create<FlyerTeleportOut>(target);
        return state;
    }

    public override void StateStart()
    {
        base.StateStart();
        animator.Play("TeleportOut");
        SetState(FlyerTeleportIn.Create(target));
    }

    public override void StateUpdate()
    {  
        //ele só para e teletransporta para o inicio do seu path
    }
}
