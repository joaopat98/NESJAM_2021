using UnityEngine;

public class Thrower : EnemyBase<Thrower>
{
    public float shootingCooldown = 3;
    [HideInInspector] public float cooldownLeft;

    protected override void Start()
    {
        base.Start();
        state = ThrowerIdle.Create(this);
    }

}