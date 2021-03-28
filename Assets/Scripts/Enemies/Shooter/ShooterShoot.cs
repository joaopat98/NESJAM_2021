using UnityEngine;

public class ShooterShoot : ShooterState
{
    float shotsFired = 0;
    float prepareCooldown;
    float bulletCooldown;
    float idleCooldown;
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
        prepareCooldown = target.TimeToPrepare;
        idleCooldown = target.TimeToPrepare;
        bulletCooldown = target.fireRate;
        animator.Play("Shoot");
    }

    public override void StateUpdate()
    {
        prepareCooldown -= Time.deltaTime;
        if (prepareCooldown <= 0)
        {
            bulletCooldown -= Time.deltaTime;
            if (bulletCooldown <= 0 && shotsFired < target.shotNumber)
            {
                var bullet = Instantiate(target.bulletPrefab, target.bulletSpawn.position, Quaternion.identity).GetComponent<Bullet>();
                bullet.Init(Vector3.right * direction);
                shotsFired++;
                bulletCooldown = target.fireRate;
            }

            if (shotsFired >= target.shotNumber)
            {
                idleCooldown -= Time.deltaTime;
                if (idleCooldown <= 0)
                {
                    SetState(ShooterIdle.Create(target));
                }
            }
        }
    }
}
