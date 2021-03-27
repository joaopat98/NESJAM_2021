using UnityEngine;

public class ShooterShoot : ShooterState
{
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
        if (target.shotsFired == target.shotNumber)
        {
            SetState(ShooterIdle.Create(target));
        }
    }
}
