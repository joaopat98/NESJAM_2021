using UnityEngine;

public class WallCrawlerWalk : WallCrawlerState
{
    //TODO por alguma forma de 
    private int direction;

    public static WallCrawlerWalk Create(WallCrawler target)
    {
        WallCrawlerWalk state = WallCrawlerState.Create<WallCrawlerWalk>(target);
        return state;
    }

    public override void StateStart()
    {
        base.StateStart();
        direction = target.startUp ? 1 : -1;
        //TODO trocar sprite
    }

    public override void StateUpdate()
    {
        renderer.flipY = direction < 0;
        transform.Translate(Vector3.up * direction * target.walkSpeed * Time.deltaTime);
        //a troca de direção ocorre quando tocas em alguma coisa
        PlayerEntity player = PlayerEntity.instance;
        if (Mathf.Abs(transform.position.y - player.transform.position.y) < target.attackRange)
        {
            SetState(WallCrawlerShoot.Create(target));
        }

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (target.directionChangers.HasLayer(collision.gameObject.layer))
        {
            direction = -direction;
        }
    }

}
