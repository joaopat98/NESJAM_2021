using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCrawler : EnemyBase<WallCrawler>
{

    public bool startUp;
    public float walkSpeed = 2f;
    public float attackRange = 0.5f;
    public Transform Head;

    public float RayDistance;

    public LayerMask directionChangers;

    protected override void Start()
    {
        base.Start();
        state = WallCrawlerWalk.Create(this);
    }
}
