using UnityEngine;

public class WallTurretShoot : WallTurretState
{
    public static WallTurretShoot Create(WallTurret target)
    {
        WallTurretShoot state = WallTurretState.Create<WallTurretShoot>(target);
        return state;
    }

    public override void StateStart()
    {
        base.StateStart();
        //TODO trocar sprite
    }

    public override void StateUpdate()
    {
        //TODO disparar tiros 

        SetState(WallTurretIdle.Create(target));
    }
}
