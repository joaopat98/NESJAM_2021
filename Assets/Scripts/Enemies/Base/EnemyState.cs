using UnityEngine;

public abstract class EnemyState : MonoBehaviour
{
    public bool Initialized { get; protected set; }
    public virtual void StateStart()
    {
        Initialized = true;
        WorldManager.OnWorldSwitch.AddListener(WorldSwitch);
    }
    public abstract void StateUpdate();

    public virtual void StateFixedUpdate() { }

    public virtual void OnGetHit() { }

    protected virtual void WorldSwitch(WorldType world) { }

}

public abstract class EnemyState<EnemyType> : EnemyState where EnemyType : EnemyBase<EnemyType>
{
    protected EnemyType target;

    protected static T Create<T>(EnemyType target) where T : EnemyState<EnemyType>
    {
        T state = target.gameObject.AddComponent<T>();
        state.target = target;
        return state;
    }

    protected void SetState(EnemyState<EnemyType> state)
    {
        target.SetState(state);
        Destroy(this);
    }

}