using UnityEngine;

public class BossSecondPhaseDarkStomp : BossSecondPhaseDarkState
{
    float t = 0;

    public static BossSecondPhaseDarkStomp Create(Boss target)
    {
        var state = BossSecondPhaseDarkState.Create<BossSecondPhaseDarkStomp>(target);
        return state;
    }



    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 toPlayer = PlayerEntity.instance.transform.position - transform.position;
            Vector2 impulseDir = new Vector2(Mathf.Sign(toPlayer.x), 0) * props.ImpulseStrength;
            Impulse impulse = new Impulse(impulseDir, props.ImpulseTime);
            PlayerEntity.instance.health.Hit(props.Damage, impulse);
        }
        if (props.HitMask.HasLayer(collision.gameObject.layer))
        {
            SetState(BossSecondPhaseDarkRecover.Create(target));
        }
    }

    public override void StateUpdate()
    {
        t += Time.deltaTime;
        if (t > props.LockOnTime)
        {
            animator.Play("Shoot");
            transform.position += Vector3.down * props.DropSpeed * Time.deltaTime;
        }
    }
}