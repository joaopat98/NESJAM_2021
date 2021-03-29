using UnityEngine;

public class WallTurretShoot : WallTurretState
{
    int direction;

    public static WallTurretShoot Create(WallTurret target)
    {
        WallTurretShoot state = WallTurretState.Create<WallTurretShoot>(target);
        return state;
    }

    public override void StateStart()
    {
        base.StateStart();
        animator.Play("Shoot");
        direction = target.facingRight ? 1 : -1;
    }

    public override void StateUpdate()
    {
        var bullet1 = Instantiate(target.bulletPrefab, target.bulletSpawn[0].position, Quaternion.identity).GetComponent<Bullet>();
        var bullet2 = Instantiate(target.bulletPrefab, target.bulletSpawn[1].position, Quaternion.identity).GetComponent<Bullet>();
        var bullet3 = Instantiate(target.bulletPrefab, target.bulletSpawn[2].position, Quaternion.identity).GetComponent<Bullet>();

        bullet1.Init(Vector3.right * direction);
        bullet2.Init((Vector3.up + Vector3.right) * direction);
        bullet3.Init((Vector3.down + Vector3.right) * direction);

        SetState(WallTurretIdle.Create(target));
    }

}
