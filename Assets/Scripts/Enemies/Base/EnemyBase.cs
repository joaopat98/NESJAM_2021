using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public int CurrentHealth;
    public int MaxHealth;

    protected virtual void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public virtual bool IsAlive { get => CurrentHealth > 0; }

    public void Call(string name)
    {
        SendMessage(name);
    }
}

public abstract class EnemyBase<EnemyType> : EnemyBase where EnemyType : EnemyBase<EnemyType>
{
    protected EnemyState<EnemyType> state;

    public void SetState(EnemyState<EnemyType> state)
    {
        this.state = state;
    }

    protected virtual void Update()
    {
        if (IsAlive)
        {
            if (!state.Initialized)
            {
                state.StateStart();
            }
            state.StateUpdate();
        }
    }
}