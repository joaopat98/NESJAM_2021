using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatforms : MonoBehaviour
{
    CharacterController2D controller;
    int frame = 0;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController2D>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.gameObject);
        if (other.collider.CompareTag("Platform"))
        {
            other.gameObject.GetComponent<Platform>().Activate();
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("Platform"))
        {
            other.gameObject.GetComponent<Platform>().Deactivate();
        }
    }
}
