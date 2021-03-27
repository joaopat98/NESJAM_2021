using UnityEngine;

public abstract class BomberState : EnemyState<Bomber>
{
    new protected SpriteRenderer renderer;
    protected Animator animator;
    protected static new T Create<T>(Bomber target) where T : BomberState
    {
        var state = EnemyState<Bomber>.Create<T>(target);
        state.renderer = target.GetComponent<SpriteRenderer>();
        state.animator = target.GetComponent<Animator>();
        return state;
    }
}