using UnityEngine;

public class ShooterIdle : ShooterState
{
    public float shootingCooldown = 3;
    private float cooldownLeft;
    public static ShooterIdle Create(Shooter target)
    {
        ShooterIdle state = ShooterState.Create<ShooterIdle>(target);
        return state;
    }

    public override void StateStart()
    {
        base.StateStart();
        cooldownLeft = shootingCooldown;
    }

    public override void StateUpdate()
    {
        //TODO trocar sprite
        cooldownLeft -= Time.deltaTime;
        if (cooldownLeft <= 0)
        {
            SetState(ShooterShoot.Create(target));
        }
    }
}
