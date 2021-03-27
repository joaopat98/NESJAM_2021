using UnityEngine;

public class StomperFlyUp : StomperState
{
    public static StomperFlyUp Create(Stomper target)
    {
        StomperFlyUp state = StomperState.Create<StomperFlyUp>(target);
        return state;
    }
    public override void StateStart()
    {
        base.StateStart();
        animator.Play("FlyUp");
    }

    public override void StateUpdate()
    {
        //TODO voltar a voar ao y inicial
    }

    void FinishFlyUp()
    {
        SetState(StomperFly.Create(target));
    }
}