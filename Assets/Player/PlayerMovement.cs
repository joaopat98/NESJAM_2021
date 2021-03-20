using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    struct MovementInput
    {
        public float Horizontal;
        public bool StartJump;
        public bool EndJump;
    }
    MovementInput input = new MovementInput();

    HashSet<object> movementLocks = new HashSet<object>();

    public bool IsMovementLocked { get => movementLocks.Count > 0; }

    CharacterController2D controller;
    float VerticalVelocity;

    #region Inspector Parameters

    [Header("Movement")]
    public float Speed = 5;

    [Header("Jumping")]
    [SerializeField] float JumpHeight = 2;
    [SerializeField] float GravityScale = 1;
    [SerializeField] float FallMultiplier = 2;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController2D>();
    }


    void Update()
    {
        Vector3 moveVel = Vector3.zero;

        if (!IsMovementLocked)
            moveVel += input.Horizontal * Speed * Vector3.right;

        //Apply Gravity

        if (VerticalVelocity < 0)
        {
            VerticalVelocity += Physics.gravity.y * GravityScale * FallMultiplier * Time.deltaTime;
        }
        else
        {
            VerticalVelocity += Physics.gravity.y * GravityScale * Time.deltaTime;
        }

        //When grounded, vertical velocity is always slightly downward

        if (controller.isGrounded)
        {
            VerticalVelocity = Physics.gravity.y * Time.deltaTime;
        }

        //Cancel jump
        if (VerticalVelocity > 0 && input.EndJump)
        {
            VerticalVelocity = 0;
        }

        //Jump when grounded
        if (controller.isGrounded && input.StartJump)
        {
            //Calculate velocity needed to reach the pretended jump height
            VerticalVelocity = Mathf.Sqrt(-JumpHeight * 2 * Physics.gravity.y * GravityScale);
        }

        moveVel += VerticalVelocity * Vector3.up;

        controller.Move(moveVel * Time.deltaTime);
    }

    void LateUpdate()
    {
        input.StartJump = false;
        input.EndJump = false;
    }

    public void LockMovement(object owner)
    {
        movementLocks.Add(owner);
    }

    public void ReleaseMovement(object owner)
    {
        movementLocks.Remove(owner);
    }

    public void OnMove(InputValue value)
    {
        input.Horizontal = value.Get<Vector2>().x;
    }

    bool previousJump;
    public void OnJump(InputValue value)
    {
        bool currentJump = value.Get<float>() == 1;
        if (!previousJump && currentJump)
        {
            input.StartJump = true;
        }
        if (previousJump && !currentJump)
        {
            input.EndJump = true;
        }
        previousJump = currentJump;
    }
}
