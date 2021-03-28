using UnityEngine;

public class WallCrawlerWalk : WallCrawlerState
{

    public static WallCrawlerWalk Create(WallCrawler target)
    {
        WallCrawlerWalk state = WallCrawlerState.Create<WallCrawlerWalk>(target);
        state.direction = target.startUp ? 1 : -1;
        return state;
    }

    public static WallCrawlerWalk Create(WallCrawler target, int dir)
    {
        WallCrawlerWalk state = WallCrawlerState.Create<WallCrawlerWalk>(target);
        state.direction = dir;
        return state;
    }

    public override void StateStart()
    {
        base.StateStart();
        //TODO trocar sprite
    }

    public override void StateUpdate()
    {
        renderer.flipY = direction > 0;
        transform.Translate(Vector3.up * direction * target.walkSpeed * Time.deltaTime);
        RaycastHit2D up = Physics2D.Raycast(target.bulletSpawn.position, -target.bulletSpawn.right, target.horizontalAttackRange, target.playerMask);

        if (up.collider != null)
        {
            SetState(WallCrawlerShoot.Create(target, direction));
        }
    }

}
