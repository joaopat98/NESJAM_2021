using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerGetHitEvent : UnityEvent { }

public class PlayerHealth : MonoBehaviour
{
    public int CurrentHealth;
    public int MaxHealth;
    public int NumLives;
    private static bool hasRan = false;
    public int CurrentLives;
    PlayerEntity player;

    public PlayerGetHitEvent OnGetHit = new PlayerGetHitEvent();


    public bool isAlive { get => CurrentHealth > 0; }

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerEntity>();
        CurrentHealth = MaxHealth;
        if (!hasRan)
        {
            CurrentLives = NumLives;
            hasRan = true;
        }
    }

    public void Hit(int damage)
    {
        CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);
        OnGetHit.Invoke();
        if (!isAlive)
        {
            CurrentHealth = MaxHealth;
            CurrentLives--;
            if (CurrentLives > 0)
            {
                Respawn();
            }
        }
    }

    public void Respawn()
    {
        Vector3 respawnPoint = RoomManager.GetCurrentRespawnPoint();
        player.controller.Teleport(respawnPoint);
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
    }
}
