using UnityEngine;

public class WallTurret : EnemyBase<WallTurret>
{
    public float shootingCooldown = 3;
    [HideInInspector] public float cooldownLeft;

    protected override void Start()
    {
        base.Start();
        state = WallTurretIdle.Create(this);
    }

}
