using UnityEngine;

public class BossSecondPhaseDarkHover : BossSecondPhaseDarkState
{
    float t = 0;
    float ShieldTimer = 0;
    bool HasShield = true;
    int direction = 1;
    new BoxCollider2D collider;
    public static BossSecondPhaseDarkHover Create(Boss target)
    {
        var state = BossSecondPhaseDarkState.Create<BossSecondPhaseDarkHover>(target);
        state.collider = target.GetComponent<BoxCollider2D>();
        return state;
    }

    public override void StateStart()
    {
        base.StateStart();
        if (!target.isClone)
        {
            target.Shield.SetActive(true);
            if (!phaseProps.started)
            {
                phaseProps.Assets.SetActive(true);
                phaseProps.started = true;
            }
            if (!props.started)
            {
                props.started = true;
                target.SecondShield.SetActive(true);
                transform.position = props.StartPos.position;
            }
        }
    }
    public override void StateUpdate()
    {
        transform.Translate(Vector3.right * direction * props.FlySpeed * Time.deltaTime);
        float deltaX = (transform.position - props.StartPos.position).x;
        if (direction * deltaX > props.FlyRange)
        {
            direction = -direction;
        }
        PlayerEntity player = PlayerEntity.instance;
        if (Mathf.Abs(transform.position.x - player.transform.position.x) < props.AttackRange)
        {
            SetState(BossSecondPhaseDarkStomp.Create(target));
        }
    }
}