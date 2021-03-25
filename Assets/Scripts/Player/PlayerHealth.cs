using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerGetHitEvent : UnityEvent { }

public class PlayerHealth : MonoBehaviour
{
    public int Health;
    public int MaxHealth;

    //Temporary -> Load Current Level
    public string SceneToLoadName;

    public int NumLives;
    private static bool hasRan = false;
    public int CurrentLives;

    public PlayerGetHitEvent OnGetHit = new PlayerGetHitEvent();


    public bool isAlive { get => Health > 0; }

    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;
        if(!hasRan){
            CurrentLives = NumLives;
            hasRan = true;
        }
    }

    public void Hit(int damage)
    {
        Health = Mathf.Max(Health - damage, 0);
        OnGetHit.Invoke();
    }

    public void Respawn(){
        SceneManager.LoadScene(SceneToLoadName);
    }

    public void GameOver(){
        Debug.Log("Game Over");
    }

    public void HasDied(){
        Debug.Log(isAlive);
        if (!isAlive){
            CurrentLives -= 1;
            if (CurrentLives <= 0){ GameOver(); } else { Respawn(); }
        }
    }
}
