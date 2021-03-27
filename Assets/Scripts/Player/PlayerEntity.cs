using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : MonoBehaviour
{
    public static PlayerEntity instance { get; private set; }
    public PlayerAnimations animations { get; private set; }
    public PlayerMovement movement { get; private set; }
    public PlayerWeapons weapons { get; private set; }
    public PlayerHealth health { get; private set; }
    public CharacterController2D controller { get; private set; }


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("Multiple players present in scene! Destroying...");
            Destroy(gameObject);
        }
        animations = GetComponent<PlayerAnimations>();
        movement = GetComponent<PlayerMovement>();
        weapons = GetComponent<PlayerWeapons>();
        health = GetComponent<PlayerHealth>();
        controller = GetComponent<CharacterController2D>();
    }

    void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
}
