using UnityEngine;

public class WallTurret : EnemyBase<WallTurret>
{
    public float shootingCooldown = 3;
    public GameObject bulletPrefab;
    public Transform[] bulletSpawn;
    public bool facingRight = false;

    protected override void Start()
    {
        base.Start();
        state = WallTurretIdle.Create(this);
    }

}
