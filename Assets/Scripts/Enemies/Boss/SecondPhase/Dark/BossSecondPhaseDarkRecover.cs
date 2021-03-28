using UnityEngine;

public class BossSecondPhaseDarkRecover : BossSecondPhaseDarkState
{

    public static BossSecondPhaseDarkRecover Create(Boss target)
    {
        var state = BossSecondPhaseDarkState.Create<BossSecondPhaseDarkRecover>(target);
        return state;
    }

    public override void StateUpdate()
    {
        if (transform.position.y < props.StartPos.position.y)
        {
            transform.position += Vector3.up * props.RecoverSpeed * Time.deltaTime;
        }
        else
        {
            SetState(BossSecondPhaseDarkHover.Create(target));
        }
    }
}