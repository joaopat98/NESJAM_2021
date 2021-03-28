using UnityEngine;

public class Thrower : EnemyBase<Thrower>
{
    public float shootingCooldown = 3f;
    public float attackRange = 5f;
    [HideInInspector] public float fireRate = 1f;
    [HideInInspector] public float shotNumber = 1f;
    public float throwStrength = 10f;
    public float TimeToPrepare = 0.5f;
    public bool facingRight = false;
    public GameObject bombPrefab;
    public Transform bombSpawn;

    protected override void Start()
    {
        base.Start();
        state = ThrowerIdle.Create(this);
    }

}