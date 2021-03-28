using UnityEngine;

public abstract class FixedEnemyState : EnemyState<FixedEnemy>
{
    protected Animator animator;
    protected static new T Create<T>(FixedEnemy target) where T : FixedEnemyState
    {
        var state = EnemyState<FixedEnemy>.Create<T>(target);
        state.animator = target.GetComponent<Animator>();
        return state;
    }
}
