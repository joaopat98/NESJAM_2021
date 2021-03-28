using UnityEngine;

public class ThrowerThrow : ThrowerState
{
    float shotsFired = 0;
    float prepareCooldown;
    float bulletCooldown;
    float idleCooldown;
    int direction;
    public static ThrowerThrow Create(Thrower target)
    {
        ThrowerThrow state = ThrowerState.Create<ThrowerThrow>(target);
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
    }

    public override void StateUpdate()
    {
        prepareCooldown -= Time.deltaTime;
        if (prepareCooldown <= 0)
        {
            bulletCooldown -= Time.deltaTime;
            if (bulletCooldown <= 0 && shotsFired < target.shotNumber)
            {
                PlayerEntity player = PlayerEntity.instance;
                Vector3 toPlayer = transform.position - player.transform.position;
                if (target.facingRight)
                {
                    toPlayer *= -1;
                }
                var bullet = Instantiate(target.bombPrefab, target.bombSpawn.position, Quaternion.identity).GetComponent<Bullet>();
                bullet.Init(toPlayer * direction);
                shotsFired++;
                bulletCooldown = target.fireRate;
            }

            if (shotsFired >= target.shotNumber)
            {
                idleCooldown -= Time.deltaTime;
                if (idleCooldown <= 0)
                {
                    SetState(ThrowerIdle.Create(target));
                }
            }
        }
    }
}
