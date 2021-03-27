using UnityEngine;

public class ShooterShoot : ShooterState
{
    float shotsFired = 0;
    float bulletCooldown = 0;
    int direction;
    public static ShooterShoot Create(Shooter target)
    {
        ShooterShoot state = ShooterState.Create<ShooterShoot>(target);
        return state;
    }

    public override void StateStart()
    {
        base.StateStart();
        //TODO trocar de sprite
        direction = target.facingRight ? 1 : -1;
        bulletCooldown = target.fireRate;
    }

    public override void StateUpdate()
    {
        bulletCooldown -= Time.deltaTime;
        if (bulletCooldown <= 0)
        {
            var bullet = Instantiate(target.bulletPrefab, target.bulletSpawn.position, Quaternion.identity).GetComponent<Bullet>();
            bullet.Init(Vector3.right * direction);
            shotsFired++;
            bulletCooldown = target.fireRate;
            
        }

        if (shotsFired == target.shotNumber)
        {
            SetState(ShooterIdle.Create(target));
        }
    }
}
