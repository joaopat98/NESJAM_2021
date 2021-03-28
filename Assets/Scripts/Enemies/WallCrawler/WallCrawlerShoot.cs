using UnityEngine;

public class WallCrawlerShoot : WallCrawlerState
{

    float prepareCooldown;
    float idleCooldown;
    bool shot;

    public static WallCrawlerShoot Create(WallCrawler target, int dir)
    {
        WallCrawlerShoot state = WallCrawlerState.Create<WallCrawlerShoot>(target);
        state.direction = dir;
        return state;
    }

    public override void StateStart()
    {
        base.StateStart();
        prepareCooldown = target.TimeToPrepare;
        idleCooldown = target.TimeToPrepare;
        shot = false;
    }

    public override void StateUpdate()
    {
        if (!shot)
        {
            prepareCooldown -= Time.deltaTime;
            if (prepareCooldown <= 0)
            {
                int bulletDirection = target.faceLeft ? -1 : 1;
                var bullet = Instantiate(target.bulletPrefab, target.bulletSpawn.position, Quaternion.identity).GetComponent<Bullet>();
                bullet.Init(transform.right * bulletDirection);
                shot = true;

            }
        }
        else
        {
            idleCooldown -= Time.deltaTime;
            if (idleCooldown <= 0)
            {
                SetState(WallCrawlerWalk.Create(target, direction));

            }
        }
    }

}