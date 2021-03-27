using UnityEngine;

public class StomperLockOn : StomperState
{
    float timer = 0;

    public static StomperLockOn Create(Stomper target)
    {
        StomperLockOn state = StomperState.Create<StomperLockOn>(target);
        return state;
    }

    public override void StateStart()
    {
        base.StateStart();
    }
    public override void StateUpdate()
    {
        timer += Time.deltaTime;
        if (timer > target.TimeToPrepare)
        {
            SetState(StomperStomp.Create(target));
        }
    }
}

