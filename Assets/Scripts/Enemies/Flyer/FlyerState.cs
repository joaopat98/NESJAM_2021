using UnityEngine;

public abstract class FlyerState : EnemyState<Flyer>
{

    new protected SpriteRenderer renderer;
    protected Animator animator;
    protected static new T Create<T>(Flyer target) where T : FlyerState
    {
        var state = EnemyState<Flyer>.Create<T>(target);
        state.renderer = target.GetComponent<SpriteRenderer>();
        state.animator = target.GetComponent<Animator>();
        return state;
    }
}
