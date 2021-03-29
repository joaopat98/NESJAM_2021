using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLadderMovement : MonoBehaviour
{
    float previousVertical;
    bool wasGrounded;
    public PlayerMovement.MovementInput ladderInput = new PlayerMovement.MovementInput();

    [HideInInspector] public bool OnLadder = false;
    Transform ladder;
    private PlayerMovement playerMovement;
    PlayerAnimations animations;

    CharacterController2D controller;
    float VerticalOutput;

    [Header("Movement")]
    public float Speed = 5;

    Vector2 moveVel;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animations = GetComponent<PlayerAnimations>();
        controller = GetComponent<CharacterController2D>();
    }

    void Update()
    {
        animations.ActiveInputThisFrame = animations.ActiveInputThisFrame || ladderInput.isActive;
        if (OnLadder)
        {
            controller.Move(Vector2.up * ladderInput.Vertical * Speed * Time.deltaTime);
            if (ladderInput.StartJump || ladder == null || (!wasGrounded && controller.isGrounded))
            {
                OnLadder = false;
                playerMovement.Release(this);
                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Ground"), false);
            }
        }
        else
        {
            if (ladder && previousVertical == 0 && ladderInput.Vertical != 0)
            {
                OnLadder = true;
                Vector2 newPos = transform.position;
                newPos.x = ladder.position.x;
                controller.Teleport(newPos);
                playerMovement.Lock(this, true);
                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Ground"), true);
            }
        }
    }

    void LateUpdate()
    {
        ladderInput.StartJump = false;
        ladderInput.EndJump = false;
        previousVertical = ladderInput.Vertical;
        wasGrounded = controller.isGrounded;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Ladder"))
        {
            ladder = collider.transform;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Ladder") && collider.transform == ladder)
        {
            ladder = null;
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

    void OnMove(InputValue value)
    {
        ladderInput.Vertical = value.Get<Vector2>().y;
    }


    bool previousJump;
    public void OnJump(InputValue value)
    {
        bool currentJump = value.Get<float>() == 1;
        if (!previousJump && currentJump)
        {
            ladderInput.StartJump = true;
        }
        if (previousJump && !currentJump)
        {
            ladderInput.EndJump = true;
        }
        previousJump = currentJump;
    }

}
