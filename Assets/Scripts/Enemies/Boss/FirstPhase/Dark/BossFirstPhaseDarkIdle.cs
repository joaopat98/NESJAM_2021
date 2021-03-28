using UnityEngine;

public class BossFirstPhaseDarkIdle : BossFirstPhaseDarkState
{
    float t = 0;
    float ShieldTimer = 0;
    bool HasShield = true;
    int direction = 1;
    new BoxCollider2D collider;
    public static BossFirstPhaseDarkIdle Create(Boss target)
    {
        var state = BossFirstPhaseDarkState.Create<BossFirstPhaseDarkIdle>(target);
        state.collider = target.GetComponent<BoxCollider2D>();
        return state;
    }

    public override void StateStart()
    {
        base.StateStart();
        if (!target.isClone)
        {
            target.Shield.SetActive(true);
            transform.position = props.StartPos.position;
            BossClone other = Instantiate(props.ClonePrefab, props.CloneStart.position, Quaternion.Euler(0, 0, 180)).GetComponent<BossClone>();
            other.InitClone(target);
            target.other = other;
        }
    }
    public override void StateUpdate()
    {
        t += Time.deltaTime;
        RaycastHit2D wallHit = Physics2D.Raycast(transform.position, transform.right * direction, collider.bounds.extents.x + 0.1f, props.GroundMask);
        if (wallHit.collider != null)
        {
            direction = -direction;
        }
        transform.Translate(transform.right * direction * props.Speed * Time.deltaTime, Space.World);
        if (t > props.TimeBetweenShots)
        {
            t = 0;
            var bullet = Instantiate(props.BulletPrefab, transform.position, Quaternion.identity).GetComponent<Bullet>();
            bullet.Init(PlayerEntity.instance.transform.position - transform.position);
        }

        if (!target.isClone)
        {
            ShieldTimer += Time.deltaTime;
            if (ShieldTimer > props.TimeWithShield)
            {
                ShieldTimer = 0;
                HasShield = !HasShield;
                target.SetShieldStatus(HasShield);
                target.other.SetShieldStatus(!HasShield);
            }
        }
    }
}