using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public int CurrentHealth;
    public int MaxHealth;
    [SerializeField] GameObject ExplosionPrefab;
    [SerializeField] Transform ExplosionSpawn;

    public bool isAlive { get => CurrentHealth > 0; }

    protected virtual void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public virtual bool IsAlive { get => CurrentHealth > 0; }

    public void Call(string name)
    {
        SendMessage(name);
    }

    public virtual void Hit(int damage)
    {
        if (isAlive)
        {
            CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);
            if (isAlive)
            {
                Die();
            }
        }
    }

    public virtual void Die()
    {
        Vector3 spawnPos = ExplosionSpawn != null ? ExplosionSpawn.position : transform.position;
        if (ExplosionPrefab != null)
        {
            Instantiate(ExplosionPrefab, spawnPos, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("ExplosionPrefab not set!");
        }
        Destroy(gameObject);
    }
}

public abstract class EnemyBase<EnemyType> : EnemyBase where EnemyType : EnemyBase<EnemyType>
{
    protected EnemyState<EnemyType> state;

    public void SetState(EnemyState<EnemyType> state)
    {
        this.state = state;
    }

    public override void Hit(int damage)
    {
        if (isAlive)
        {
            CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);
            state.OnGetHit();
            if (isAlive)
            {
                Die();
            }
        }
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

    protected virtual void FixedUpdate()
    {
        if (IsAlive)
        {
            if (!state.Initialized)
            {
                state.StateStart();
            }
            state.StateFixedUpdate();
        }
    }
}