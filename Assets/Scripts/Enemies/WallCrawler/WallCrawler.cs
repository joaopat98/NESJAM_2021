using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCrawler : EnemyBase<WallCrawler>
{

    public bool startUp = false;
    public bool faceLeft = false;
    public float walkSpeed = 2f;
    public float horizontalAttackRange = 10f;
    public float TimeToPrepare = 0.5f;

    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    //public float RayDistance;

    public LayerMask directionChangers;
    public LayerMask playerMask;

    protected override void Start()
    {
        base.Start();
        state = WallCrawlerWalk.Create(this);
    }
}
