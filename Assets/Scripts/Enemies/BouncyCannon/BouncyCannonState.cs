using UnityEngine;

public abstract class BouncyCannonState : EnemyState<BouncyCannon>
{
    new protected SpriteRenderer renderer;
    protected static new T Create<T>(BouncyCannon target) where T : BouncyCannonState
    {
        var state = EnemyState<BouncyCannon>.Create<T>(target);
        state.renderer = target.GetComponent<SpriteRenderer>();
        return state;
    }
}
