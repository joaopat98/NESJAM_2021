using UnityEngine;

public class WallTurretIdle : WallTurretState
{
    float cooldownLeft;

    public static WallTurretIdle Create(WallTurret target)
    {
        WallTurretIdle state = WallTurretState.Create<WallTurretIdle>(target);
        return state;
    }

    public override void StateStart()
    {
        base.StateStart();
        cooldownLeft = target.shootingCooldown;
        animator.Play("Idle");
    }

    public override void StateUpdate()
    {
        cooldownLeft -= Time.deltaTime;
        if (cooldownLeft <= 0)
        {
            SetState(WallTurretShoot.Create(target));
        }
    }
}
