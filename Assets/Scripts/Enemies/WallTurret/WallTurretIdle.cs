using UnityEngine;

public class WallTurretIdle : WallTurretState
{
    public static WallTurretIdle Create(WallTurret target)
    {
        WallTurretIdle state = WallTurretState.Create<WallTurretIdle>(target);
        return state;
    }

    public override void StateStart()
    {
        base.StateStart();
        target.cooldownLeft = target.shootingCooldown;
        //TODO trocar sprite
    }

    public override void StateUpdate()
    {
        target.cooldownLeft -= Time.deltaTime;
        if (target.cooldownLeft <= 0)
        {
            SetState(WallTurretShoot.Create(target));
        }
    }
}
