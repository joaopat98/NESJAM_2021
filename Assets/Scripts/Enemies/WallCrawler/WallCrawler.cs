using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCrawler : EnemyBase<WallCrawler>
{

    public bool startUp;

    protected override void Start()
    {
        base.Start();
        state = WallCrawlerWalk.Create(this);
    }
}
