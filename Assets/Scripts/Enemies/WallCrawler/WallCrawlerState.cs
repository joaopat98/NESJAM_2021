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
}
