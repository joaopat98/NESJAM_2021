using UnityEngine;

public class BouncyCannonIdle : BouncyCannonState
{
    public static BouncyCannonIdle Create(BouncyCannon target)
    {
        BouncyCannonIdle state = BouncyCannonState.Create<BouncyCannonIdle>(target);
        return state;
    }

    public override void StateStart()
    {
        base.StateStart();
        target.cooldownLeft = target.shootingCooldown;
        //TODO trocar sprite
    }

    public override void StateUpdate()
    {
        target.cooldownLeft -= Time.deltaTime;
        if (target.cooldownLeft <= 0)
        {
            SetState(BouncyCannonShoot.Create(target));
        }
    }
}
