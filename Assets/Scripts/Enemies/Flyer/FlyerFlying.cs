using UnityEngine;

public class FlyerFlying : FlyerState
{
    public float relativeX;
    public static FlyerFlying Create(Flyer target)
    {
        FlyerFlying state = FlyerState.Create<FlyerFlying>(target);
        return state;
    }

    public override void StateStart()
    {
        base.StateStart();
    }

    public override void StateUpdate()
    {
        relativeX -= target.flySpeed * Time.deltaTime;
        //float relativeY = target.waveAmplitude * Mathf.Cos(1 / target.wavePeriod * relativeX);
        float relativeY = target.waveAmplitude * Mathf.Acos(Mathf.Cos(1 / target.wavePeriod * relativeX));
        transform.position = target.startPosition + new Vector3(relativeX, relativeY, 0);
        if (Mathf.Abs(this.transform.position.x - target.startPosition.x) >= target.flightRange)
        {
            SetState(FlyerTeleportOut.Create(target));
        }
    }
}
