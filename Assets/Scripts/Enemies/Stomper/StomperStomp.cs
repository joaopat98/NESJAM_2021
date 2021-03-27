using UnityEngine;

public class StomperStomp : StomperState
{
    float timer = 0;
    bool dropped;

    public static StomperStomp Create(Stomper target)
    {
        StomperStomp state = StomperState.Create<StomperStomp>(target);
        return state;
    }

    public override void StateStart()
    {
        base.StateStart();
        animator.Play("Prepare");
    }
    public override void StateUpdate()
    {
        //TODO pisar o player
        if (!dropped)
        {
            timer += Time.deltaTime;
            if (timer > target.TimeToPrepare)
            {
                animator.Play("Stomp");
                dropped = true;
            }
        }
        //TODO meter um if para ser quando ele tocar no chao/player
        SetState(StomperFlyUp.Create(target));
    }

}