using UnityEngine;

public class ShooterIdle : ShooterState
{
    float cooldownLeft;
    public static ShooterIdle Create(Shooter target)
    {
        ShooterIdle state = ShooterState.Create<ShooterIdle>(target);
        return state;
    }

    public override void StateStart()
    {
        base.StateStart();
        cooldownLeft = target.shootingCooldown;
        //TODO trocar sprite
    }

    public override void StateUpdate()
    {
        cooldownLeft -= Time.deltaTime;
        if (cooldownLeft <= 0)
        {
            SetState(ShooterShoot.Create(target));
        }
    }
}
