using UnityEngine;

public class Bomber : EnemyBase<Bomber>
{
    public float FlySpeed = 1;
    public float FlyRange = 5;
    public float TimeToPrepare = 0.5f;
    public float AttackRange = 1;
    public Transform BombSpawn;
    public GameObject BombPrefab;

    [HideInInspector] public Vector3 StartPos;

    protected override void Start()
    {
        base.Start();
        StartPos = transform.position;
        state = BomberFly.Create(this);
    }

}