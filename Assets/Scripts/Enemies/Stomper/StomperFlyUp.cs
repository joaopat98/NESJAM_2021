using UnityEngine;

public class StomperFlyUp : StomperState
{
    float timer = 0;

    public static StomperFlyUp Create(Stomper target)
    {
        StomperFlyUp state = StomperState.Create<StomperFlyUp>(target);
        return state;
    }
    public override void StateStart()
    {
        base.StateStart();
        //animator.Play("Fly");
    }

    public override void StateUpdate()
    {
        timer += Time.deltaTime;
        if (timer > target.TimeToPrepare)
        {
            transform.Translate(Vector3.up * target.FlyUpSpeed * Time.deltaTime);
            if (transform.position.y >= target.StartPos.y)
            {
                SetState(StomperFly.Create(target));
            }
        }
    }
}