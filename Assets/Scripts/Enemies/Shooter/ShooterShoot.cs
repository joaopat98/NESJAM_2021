using UnityEngine;

public class ShooterShoot : ShooterState
{
    public float shotNumber = 3;
    public float fireRate = 1;
    private float shotsFired = 0;
    public static ShooterShoot Create(Shooter target)
    {
        ShooterShoot state = ShooterState.Create<ShooterShoot>(target);
        return state;
    }

    public override void StateStart()
    {
        base.StateStart();
    }

    public override void StateUpdate()
    {
        //TODO disparar tiros e trocar sprite
        if (shotsFired == shotNumber)
        {
            SetState(ShooterIdle.Create(target));
        }
    }
}
