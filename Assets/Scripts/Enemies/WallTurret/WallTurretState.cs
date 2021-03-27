using UnityEngine;

public abstract class WallTurretState : EnemyState<WallTurret>
{
    new protected SpriteRenderer renderer;
    protected static new T Create<T>(WallTurret target) where T : WallTurretState
    {
        var state = EnemyState<WallTurret>.Create<T>(target);
        state.renderer = target.GetComponent<SpriteRenderer>();
        return state;
    }
}
