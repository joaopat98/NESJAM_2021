using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public struct MovementInput
    {
        public float Horizontal;
        public float Vertical;
        public bool StartJump;
        public bool EndJump;

        public bool isActive
        {
            get
            {
                return Horizontal != 0
                    || Vertical != 0
                    || StartJump;
            }
        }
    }

    public bool IsJumping = false;
    private bool forcedJump = false;
    public MovementInput input = new MovementInput();

    HashSet<object> movementLocks = new HashSet<object>();

    HashSet<object> verticalLocks = new HashSet<object>();

    public bool IsMovementLocked { get => movementLocks.Count > 0; }
    public bool IsVerticalLocked { get => verticalLocks.Count > 0; }

    CharacterController2D controller;
    float VerticalVelocity;

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

    PlayerAnimations animations;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController2D>();
        controller.OnAddImpulse.AddListener(_ => VerticalVelocity = 0);
        animations = GetComponent<PlayerAnimations>();
    }


    void Update()
    {
        animations.ActiveInputThisFrame = animations.ActiveInputThisFrame || input.isActive;

        moveVel = Vector3.zero;
        HorizontalOutput = 0;
        if (!IsMovementLocked)
        {
            HorizontalOutput = input.Horizontal;
            moveVel += input.Horizontal * Speed * Vector2.right;
            if (input.Horizontal != 0)
            {
                //transform.rotation = Quaternion.Euler(0, input.Horizontal > 0 ? 0 : 180, 0);
            }
        }

        if (!IsVerticalLocked)
        {
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
            if (controller.isGrounded && VerticalVelocity < 0)
            {
                VerticalVelocity = 0;
            }

            if (controller.hasCeiling && VerticalVelocity > 0)
            {
                VerticalVelocity = 0;
            }

            //Cancel jump
            if (VerticalVelocity > 0 && input.EndJump && !forcedJump)
            {
                VerticalVelocity = 0;
            }

            //Jump when grounded
            if (controller.isGrounded && input.StartJump)
            {
                Jump();
            }
            moveVel += VerticalVelocity * Vector2.up;
        }


        controller.Move(moveVel * Time.deltaTime);
        //controller.Move(moveVel * Time.deltaTime);
        //rb.MovePosition(moveVel * Time.deltaTime);
    }

    void LateUpdate()
    {
        input.StartJump = false;
        input.EndJump = false;

        if (controller.isGrounded)
        {
            forcedJump = false;
        }
    }

    public void Lock(object owner, bool vertical = false)
    {
        movementLocks.Add(owner);
        if (vertical)
        {
            verticalLocks.Add(owner);
        }

    }

    public void Release(object owner)
    {
        if (movementLocks.Contains(owner))
            movementLocks.Remove(owner);
        if (verticalLocks.Contains(owner))
            verticalLocks.Remove(owner);
    }

    public void Jump()
    {
        Jump(JumpHeight);
    }

    public void Jump(float height)
    {
        //Calculate velocity needed to reach the pretended jump height
        VerticalVelocity = Mathf.Sqrt(-height * 2 * Physics.gravity.y * GravityScale);
    }

    public void ForcedJump(float height)
    {
        forcedJump = true;
        Jump(height);
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
            IsJumping = true;
        }
        if (previousJump && !currentJump)
        {
            input.EndJump = true;
            IsJumping = false;
        }
        previousJump = currentJump;
    }
}
