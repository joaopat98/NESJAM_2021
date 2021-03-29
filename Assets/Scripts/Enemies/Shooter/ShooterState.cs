using UnityEngine;

public abstract class ShooterState : EnemyState<Shooter>
{
    new protected SpriteRenderer renderer;
    protected Animator animator;
    protected static new T Create<T>(Shooter target) where T : ShooterState
    {
        var state = EnemyState<Shooter>.Create<T>(target);
        state.renderer = target.GetComponent<SpriteRenderer>();
        state.animator = target.GetComponent<Animator>();
        return state;
    }
}
