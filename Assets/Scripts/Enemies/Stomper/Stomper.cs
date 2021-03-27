using UnityEngine;

public class Stomper : EnemyBase<Stomper>
{
    public float FlySpeed = 1;
    public float FlyRange = 5;
    public float TimeToPrepare = 0.5f;
    public float AttackRange = 1;

    [HideInInspector] public Vector3 StartPos;

    protected override void Start()
    {
        base.Start();
        StartPos = transform.position;
        state = StomperFly.Create(this);
    }

}