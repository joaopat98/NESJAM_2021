using UnityEngine;

public abstract class ThrowerState : EnemyState<Thrower>
{

    new protected SpriteRenderer renderer;
    protected Animator animator;
    protected static new T Create<T>(Thrower target) where T : ThrowerState
    {
        var state = EnemyState<Thrower>.Create<T>(target);
        state.renderer = target.GetComponent<SpriteRenderer>();
        state.animator = target.GetComponent<Animator>();
        return state;
    }
}
