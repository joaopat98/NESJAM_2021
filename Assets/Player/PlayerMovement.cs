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
    CustomCharacterController2D controller2D;
    float VerticalVelocity;
    bool startGoingUp = false;

    #region Inspector Parameters

    [Header("Movement")]
    public float Speed = 5;

    [Header("Jumping")]
    [SerializeField] float JumpHeight = 2;
    [SerializeField] float GravityScale = 1;
    [SerializeField] float FallMultiplier = 2;
    [HideInInspector] public float HorizontalOutput;
    Rigidbody2D rb;
    Vector2 moveVel;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        controller2D = GetComponent<CustomCharacterController2D>();
        //controller = GetComponent<CharacterController2D>();
    }


    void Update()
    {
        moveVel = Vector3.zero;
        HorizontalOutput = 0;
        if (!IsMovementLocked)
        {
            HorizontalOutput = input.Horizontal;
            moveVel += input.Horizontal * Speed * Vector2.right;
            if (input.Horizontal != 0)
            {
                transform.rotation = Quaternion.Euler(0, input.Horizontal > 0 ? 0 : 180, 0);
            }
        }

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
        if (controller2D.isGrounded)
        {
            VerticalVelocity = 0;
        }

        //Cancel jump
        if (VerticalVelocity > 0 && input.EndJump)
        {
            VerticalVelocity = 0;
        }

        //Jump when grounded
        if (controller2D.isGrounded && input.StartJump)
        {
            //Calculate velocity needed to reach the pretended jump height
            VerticalVelocity = Mathf.Sqrt(-JumpHeight * 2 * Physics.gravity.y * GravityScale);
        }
        moveVel += VerticalVelocity * Vector2.up;

        controller2D.Move(moveVel * Time.deltaTime);
        //controller.Move(moveVel * Time.deltaTime);
        //rb.MovePosition(moveVel * Time.deltaTime);
    }

    void LateUpdate()
    {
        input.StartJump = false;
        input.EndJump = false;
        startGoingUp = false;
    }

    public void Lock(object owner)
    {
        movementLocks.Add(owner);
    }

    public void Release(object owner)
    {
        movementLocks.Remove(owner);
    }

    public void AddVerticalVel(float value)
    {
        if (controller.isGrounded)
        {
            VerticalVelocity = Mathf.Sqrt(-value * 2 * Physics.gravity.y * GravityScale);
            startGoingUp = true;
        }
        else
        {
            //Not sure if the right math
            VerticalVelocity += Mathf.Sqrt(-value * 2 * Physics.gravity.y * GravityScale);
        }
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
