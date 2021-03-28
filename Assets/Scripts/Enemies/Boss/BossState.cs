using UnityEngine;

public abstract class BossState : EnemyState<Boss>
{
    new protected SpriteRenderer renderer;
    protected Animator animator;
    protected static new T Create<T>(Boss target) where T : BossState
    {
        var state = EnemyState<Boss>.Create<T>(target);
        state.renderer = target.GetComponent<SpriteRenderer>();
        state.animator = target.GetComponent<Animator>();
        return state;
    }
}