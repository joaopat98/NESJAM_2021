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
    public int shotsPerLife = 5;
    private int shotsHit;
    public float InvincibilityFrame;
    [SerializeField] float BlinkFrequency = 4;
    public float TimeToRecover;
    private static bool hasRan = false;
    public int CurrentLives;
    PlayerEntity player;
    new SpriteRenderer renderer;
    public bool GettingHit { get; private set; }
    public bool Invincible { get; private set; }

    public PlayerGetHitEvent OnGetHit = new PlayerGetHitEvent();

    private AudioManager audioManager;


    public bool isAlive { get => CurrentHealth > 0; }

    // Start is called before the first frame update
    void Start()
    {
        audioManager = AudioManager.instance;

        player = GetComponent<PlayerEntity>();
        renderer = GetComponent<SpriteRenderer>();
        CurrentHealth = MaxHealth;
        if (!hasRan)
        {
            CurrentLives = NumLives;
            hasRan = true;
        }
    }

    public void Hit(int damage)
    {
        Hit(damage, null);
    }

    public void Hit(int damage, Impulse impulse)
    {
        if (!Invincible)
        {
            CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);
            OnGetHit.Invoke();
            StartCoroutine(Invincibility());
            if (!isAlive)
            {
                audioManager.Play("PlayerDeath");

                CurrentHealth = MaxHealth;
                CurrentLives--;
                if (CurrentLives > 0)
                {
                    Respawn();
                }
                else
                {
                    hasRan = false;
                    SceneManager.LoadScene("Scenes/GameOver");
                }
            }
            else
            {
                audioManager.Play("TakeDamage");

                if (impulse != null)
                {
                    player.controller.AddImpulse(impulse);
                }
                StartCoroutine(GetHit());
            }
        }
    }

    IEnumerator GetHit()
    {
        GettingHit = true;
        yield return new WaitForSeconds(TimeToRecover);
        GettingHit = false;
    }

    IEnumerator Invincibility()
    {
        Invincible = true;
        Color color = renderer.color;
        float t = 0;
        while (t < InvincibilityFrame)
        {
            Color c = color;
            c.a = 1 - Mathf.FloorToInt(t * BlinkFrequency) % 2;
            renderer.color = c;
            yield return 0;
            t += Time.deltaTime;
        }
        renderer.color = color;
        Invincible = false;
    }

    public void Respawn()
    {
        Vector3 respawnPoint = RoomManager.GetCurrentRespawnPoint();
        player.controller.Teleport(respawnPoint);
        WorldManager.SwitchWorld();
    }

    public void Instakill()
    {
        audioManager.Play("PlayerDeath");

        CurrentHealth = MaxHealth;
        CurrentLives--;
        if (CurrentLives > 0)
        {
            Respawn();
        }
        else
        {
            hasRan = false;
            SceneManager.LoadScene("Scenes/GameOver");
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
    }

    public void AddHitScore()
    {
        Debug.Log("Hit");
        if (CurrentLives < NumLives)
            shotsHit++;
        if (shotsHit >= shotsPerLife)
        {
                CurrentLives++;
                shotsHit = 0;
        }
    }
}
