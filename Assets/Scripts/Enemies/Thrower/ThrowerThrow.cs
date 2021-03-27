using UnityEngine;

public class ThrowerThrow : ThrowerState
{
    public static ThrowerThrow Create(Thrower target)
    {
        ThrowerThrow state = ThrowerState.Create<ThrowerThrow>(target);
        return state;
    }

    public override void StateStart()
    {
        base.StateStart();
        //TODO trocar sprite/animação
    }

    public override void StateUpdate()
    {
        //TODO atirar uma bomba
        SetState(ThrowerIdle.Create(target));
    }
}
