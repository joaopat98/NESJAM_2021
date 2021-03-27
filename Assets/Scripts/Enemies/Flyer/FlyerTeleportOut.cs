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
        //animator.Play("TeleportOut");
        //TODO dar timing disto melhor com a animação
        Invoke("SwitchStates", 2f);
    }
    public void SwitchStates()
    {
        SetState(FlyerFlying.Create(target));
    }

    public override void StateUpdate()
    {  
    }
}
