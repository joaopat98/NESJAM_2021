using UnityEngine;

public abstract class CaterpillarState : EnemyState<Caterpillar>
{
    protected Animator animator;
    protected static new T Create<T>(Caterpillar target) where T : CaterpillarState
    {
        var state = EnemyState<Caterpillar>.Create<T>(target);
        state.animator = target.GetComponent<Animator>();
        return state;
    }
}
