using UnityEngine;

public class BouncyCannonShoot : BouncyCannonState
{
    public static BouncyCannonShoot Create(BouncyCannon target)
    {
        BouncyCannonShoot state = BouncyCannonState.Create<BouncyCannonShoot>(target);
        return state;
    }

    public override void StateStart()
    {
        base.StateStart();
    }

    public override void StateUpdate()
    {
        //TODO disparar tiros e trocar sprite
        if (target.shotsFired == target.shotNumber)
        {
            SetState(BouncyCannonIdle.Create(target));
        }
    }
}
