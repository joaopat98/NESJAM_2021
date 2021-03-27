using UnityEngine;

public class ThrowerIdle : ThrowerState
{
    public static ThrowerIdle Create(Thrower target)
    {
        ThrowerIdle state = ThrowerState.Create<ThrowerIdle>(target);
        return state;
    }

    public override void StateStart()
    {
        base.StateStart();
        target.cooldownLeft = target.shootingCooldown;
        //TODO trocar sprite/animação
    }

    public override void StateUpdate()
    {
        target.cooldownLeft -= Time.deltaTime;
        if (target.cooldownLeft <= 0)
        {
            SetState(ThrowerThrow.Create(target));
        }
    }
}
