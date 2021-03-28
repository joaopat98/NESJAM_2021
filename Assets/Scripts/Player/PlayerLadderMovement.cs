using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLadderMovement : MonoBehaviour
{
    public PlayerMovement.MovementInput ladderInput = new PlayerMovement.MovementInput();

    [HideInInspector] public bool OnLadder = false;
    private PlayerMovement playerMovement;

    CharacterController2D controller;
    float VerticalOutput;

    [Header("Movement")]
    public float Speed = 5;

    Vector2 moveVel;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        controller = GetComponent<CharacterController2D>();
    }

    void Update()
    {
        if (OnLadder)
        {
            moveVel = Vector3.zero;
            moveVel += ladderInput.Vertical * Speed * Vector2.up;

            controller.Move(moveVel * Time.deltaTime);
        }
    }

    public void EnterLadder()
    {
        playerMovement.Lock(this, true);
        OnLadder = true;
    }

    public void ExitLadder()
    {
        playerMovement.Release(this);
        OnLadder = false;
    }

    public void OnMove(InputValue value)
    {
        ladderInput.Vertical = value.Get<Vector2>().y;
    }

}
