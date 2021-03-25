using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : MonoBehaviour
{
    public PlayerAnimations animations { get; private set; }
    public PlayerMovement movement { get; private set; }
    public PlayerWeapons weapons { get; private set; }
    public PlayerHealth health { get; private set; }
    public CharacterController2D controller { get; private set; }

    void Awake()
    {
        animations = GetComponent<PlayerAnimations>();
        movement = GetComponent<PlayerMovement>();
        weapons = GetComponent<PlayerWeapons>();
        health = GetComponent<PlayerHealth>();
        controller = GetComponent<CharacterController2D>();
    }
}
