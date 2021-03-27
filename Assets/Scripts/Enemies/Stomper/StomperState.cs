using UnityEngine;

public abstract class StomperState : EnemyState<Stomper>
{
    new protected SpriteRenderer renderer;
    protected Animator animator;
    protected static new T Create<T>(Stomper target) where T : StomperState
    {
        var state = EnemyState<Stomper>.Create<T>(target);
        state.renderer = target.GetComponent<SpriteRenderer>();
        state.animator = target.GetComponent<Animator>();
        return state;
    }
}