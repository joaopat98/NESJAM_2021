using UnityEngine;

public class BossThirdPhaseShoot : BossThirdPhaseState
{
    float t;
    bool shot = false;

    public static BossThirdPhaseShoot Create(Boss target)
    {
        var state = BossThirdPhaseState.Create<BossThirdPhaseShoot>(target);
        return state;
    }

    public override void StateUpdate()
    {
        if (!shot)
        {
            animator.Play("Shoot");
            shot = true;
            foreach (var dir in props.Teleports[props.currentTeleport].GetDirections())
            {
                var bullet = Instantiate(props.BulletPrefab, transform.position, Quaternion.identity).GetComponent<Bullet>();
                bullet.Init(dir);
            }
        }
        t += Time.deltaTime;
        if (t > props.TimeAfterShot)
        {
            TeleportToNext();
            SetState(BossThirdPhaseIdle.Create(target));
        }
    }
}