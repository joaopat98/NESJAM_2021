using UnityEngine;

public abstract class WallCrawlerState : EnemyState<WallCrawler>
{
    new protected SpriteRenderer renderer;
    protected Animator animator;
    protected int direction;
    protected static new T Create<T>(WallCrawler target) where T : WallCrawlerState
    {
        var state = EnemyState<WallCrawler>.Create<T>(target);
        state.renderer = target.GetComponent<SpriteRenderer>();
        state.animator = target.GetComponent<Animator>();
        return state;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (target.directionChangers.HasLayer(collision.gameObject.layer))
        {
            direction = -direction;
        }
    }

}
