using UnityEngine;

public class BossFirstPhaseLightThrow : BossFirstPhaseLightState
{
    float t;
    int currentBomb;

    public static BossFirstPhaseLightThrow Create(Boss target)
    {
        var state = BossFirstPhaseLightState.Create<BossFirstPhaseLightThrow>(target);
        return state;
    }
    public override void StateUpdate()
    {
        t += Time.deltaTime;
        if (t > props.TimeBetweenBombs)
        {
            t = 0;
            currentBomb++;
            var bomb = Instantiate(props.BombPrefab, transform.position, Quaternion.identity).GetComponent<ThrowableBomb>();
            bomb.Init(PlayerEntity.instance.transform.position - transform.position);
        }
        if (currentBomb >= props.NumBombs)
        {
            SetState(BossFirstPhaseLightIdle.Create(target));
        }
    }
}