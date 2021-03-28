using UnityEngine;

public class WallCrawlerShoot : WallCrawlerState
{

    public static WallCrawlerShoot Create(WallCrawler target)
    {
        WallCrawlerShoot state = WallCrawlerState.Create<WallCrawlerShoot>(target);
        return state;
    }

    public static WallCrawlerShoot Create(WallCrawler target, int dir)
    {
        WallCrawlerShoot state = WallCrawlerState.Create<WallCrawlerShoot>(target);
        state.direction = dir;
        return state;
    }

    public override void StateStart()
    {
        base.StateStart();
    }

    public override void StateUpdate()
    {
        //TODO disparar tiro
        SetState(WallCrawlerWalk.Create(target, direction));
    }
}
