using UnityEngine;

public class BossSecondPhaseLightThrow : BossSecondPhaseLightState
{
    float t;

    public static BossSecondPhaseLightThrow Create(Boss target)
    {
        var state = BossSecondPhaseLightState.Create<BossSecondPhaseLightThrow>(target);
        return state;
    }

    public override void StateStart()
    {
        base.StateStart();
        if (!phaseProps.started)
        {
            phaseProps.started = true;
            phaseProps.Assets.SetActive(true);
        }
        if (!props.started)
        {
            props.started = true;
            transform.position = props.StartPoint.position;
        }
    }

    public override void StateUpdate()
    {
        t += Time.deltaTime;
        if (t > props.TimeBetweenBombs)
        {
            t = 0;
            var bomb = Instantiate(props.BombPrefab, transform.position, Quaternion.identity).GetComponent<ThrowableBomb>();
            bomb.Init(PlayerEntity.instance.transform.position - transform.position);
        }
    }
}