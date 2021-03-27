using UnityEngine;

public abstract class ShooterState : EnemyState<Shooter>
{
    new protected SpriteRenderer renderer;
    protected static new T Create<T>(Shooter target) where T :ShooterState
    {
        var state = EnemyState<Shooter>.Create<T>(target);
        state.renderer = target.GetComponent<SpriteRenderer>();
        return state;
    }
}
