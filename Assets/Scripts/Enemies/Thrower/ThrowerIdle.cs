using UnityEngine;

public class ThrowerIdle : ThrowerState
{
    float cooldownLeft;

    public static ThrowerIdle Create(Thrower target)
    {
        ThrowerIdle state = ThrowerState.Create<ThrowerIdle>(target);
        return state;
    }

    public override void StateStart()
    {
        base.StateStart();
        cooldownLeft = target.shootingCooldown;
        //TODO trocar sprite/animação
    }

    public override void StateUpdate()
    {
        cooldownLeft -= Time.deltaTime;
        if (cooldownLeft <= 0)
        {
            SetState(ThrowerThrow.Create(target));
        }
    }
}
