using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PlayerGetHitEvent : UnityEvent { }

public class PlayerHealth : MonoBehaviour
{
    public int Health;
    public int MaxHealth;

    public int NumLives;

    public PlayerGetHitEvent OnGetHit = new PlayerGetHitEvent();


    public bool isAlive { get => Health <= 0; }

    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;
    }

    public void Hit(int damage)
    {
        Health = Mathf.Max(Health - damage, 0);
        OnGetHit.Invoke();
    }
}
