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
        transform.position = target.startPosition;
        //animator.Play("TeleportIn");
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
