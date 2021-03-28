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
        //direction = target.startUp ? 1 : -1;
        //TODO trocar sprite
    }

    public override void StateUpdate()
    {
        //renderer.flipY = direction > 0;
        Debug.Log(direction);
        transform.Translate(Vector3.up * direction * target.walkSpeed * Time.deltaTime);
        //a troca de direção ocorre quando tocas em alguma coisa
        PlayerEntity player = PlayerEntity.instance;
        if (Mathf.Abs(transform.position.y - player.transform.position.y) < target.verticalAttackRange)
        {
            SetState(WallCrawlerShoot.Create(target, direction));
        }
        /*
        RaycastHit2D down = Physics2D.Raycast(target.Head.position, direction * target.Head.up, target.RayDistance, target.directionChangers);
        if (down.collider != null)
        {
            Debug.Log("Hi");
            direction = -direction;
            transform.Rotate(Vector3.forward, 180);
        }
        */
    }

    /*
    void CheckForWallCollision()
    {
        RaycastHit2D down = Physics2D.Raycast(target.Head.position, direction * target.Head.up, target.RayDistance, target.directionChangers);
        if (down.collider != null)
        {
            Debug.Log("Direction ");
            direction = -direction;
            Debug.Log(direction);
            transform.Rotate(Vector3.forward, 180);
        }
    }
    */
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (target.directionChangers.HasLayer(collision.gameObject.layer))
        {
            Debug.Log(target.Head);
            direction = -direction;
        }
    } 

}
