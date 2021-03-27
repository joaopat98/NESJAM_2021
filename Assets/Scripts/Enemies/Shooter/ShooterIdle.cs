using UnityEngine;

public class ShooterIdle : ShooterState
{
    public static ShooterIdle Create(Shooter target)
    {
        ShooterIdle state = ShooterState.Create<ShooterIdle>(target);
        return state;
    }

    public override void StateStart()
    {
        base.StateStart();
        target.cooldownLeft = target.shootingCooldown;
    }

    public override void StateUpdate()
    {
        //TODO trocar sprite
        target.cooldownLeft -= Time.deltaTime;
        if (target.cooldownLeft <= 0)
        {
            SetState(ShooterShoot.Create(target));
        }
    }
}
