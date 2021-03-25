using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] Transform flipParent;
    PlayerMovement movement;
    CharacterController2D controller;
    Animator anim;
    new SpriteRenderer renderer;

    bool MovedStateThisFrame;

    bool AnimationFinished
    {
        get
        {
            return anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && !MovedStateThisFrame;
        }
    }
    bool MovedState;
    public bool ActiveInputThisFrame;

    [HideInInspector] public PlayerAnimationState Idle, Run, Buster, Jump, Fall;
    public bool ShootBuster;
    public PlayerAnimationState current;
    // Start is called before the first frame update
    void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        controller = GetComponent<CharacterController2D>();
        anim = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();

        System.Func<bool> ShootBusterCondition = () =>
        {
            if (ShootBuster == true)
            {
                ShootBuster = false;
                return true;
            }
            return false;
        };

        PlayerAnimationStateTransition JumpTransition = new PlayerAnimationStateTransition
        {
            To = () => Jump,
            Condition = () => !controller.isGrounded
        };

        PlayerAnimationStateTransition BusterTransition = new PlayerAnimationStateTransition
        {
            To = () => Buster,
            Condition = ShootBusterCondition
        };

        Idle = new PlayerAnimationState
        {
            StateName = "Idle",
            Loop = true,
            ImmediateTransitions = new List<PlayerAnimationStateTransition> {
                new PlayerAnimationStateTransition {
                    To = () => Run,
                    Condition = () => movement.HorizontalOutput != 0
                },
                BusterTransition,
                JumpTransition,
            }
        };

        Run = new PlayerAnimationState
        {
            StateName = "Run",
            Loop = true,
            ImmediateTransitions = new List<PlayerAnimationStateTransition> {
                new PlayerAnimationStateTransition {
                    To = () => Idle,
                    Condition = () => movement.HorizontalOutput == 0
                },
                BusterTransition,
                JumpTransition,
            }
        };

        Buster = new PlayerAnimationState
        {
            StateName = "Buster",
            Loop = false,
            ImmediateTransitions = new List<PlayerAnimationStateTransition> {
                new PlayerAnimationStateTransition {
                    To = () => Run,
                    Condition = () => AnimationFinished && ActiveInputThisFrame
                },
                BusterTransition,
                new PlayerAnimationStateTransition {
                    To = () => Jump,
                    Condition = () => !controller.isGrounded && movement.input.StartJump
                }
            }
        };

        Jump = new PlayerAnimationState
        {
            StateName = "Jump",
            Loop = true,
            ImmediateTransitions = new List<PlayerAnimationStateTransition> {
                new PlayerAnimationStateTransition {
                    To = () => Idle,
                    Condition = () => controller.isGrounded
                },
                BusterTransition,
                new PlayerAnimationStateTransition {
                    To = () => Fall,
                    Condition = () => controller.velocity.y < 0
                },
            }
        };

        Fall = new PlayerAnimationState
        {
            StateName = "Fall",
            Loop = true,
            ImmediateTransitions = new List<PlayerAnimationStateTransition> {
                new PlayerAnimationStateTransition {
                    To = () => Idle,
                    Condition = () => controller.isGrounded
                },
                BusterTransition,
            }
        };

        current = null;
        MoveToState(Idle);
    }

    void MoveToState(PlayerAnimationState state)
    {
        if (current != null && current != state)
            current.OnStateExit.Invoke();
        current = state;
        Debug.Log(current.StateName);
        if (current != state)
            current.OnStateEnter.Invoke();
        current.Play(anim);
        MovedState = true;
        MovedStateThisFrame = true;
    }

    void LateUpdate()
    {
        MovedStateThisFrame = false;
        do
        {
            MovedState = false;
            if (current.ImmediateTransitions != null)
            {
                for (int i = 0; i < current.ImmediateTransitions.Count; i++)
                {
                    var transition = current.ImmediateTransitions[i];
                    if (transition.Condition())
                    {
                        MoveToState(transition.To());
                        break;
                    }
                }
            }
            if (!current.Loop && AnimationFinished && current.EndTransitions != null)
            {
                for (int i = 0; i < current.EndTransitions.Count; i++)
                {
                    var transition = current.EndTransitions[i];
                    if (transition.Condition == null || transition.Condition())
                    {
                        MoveToState(transition.To());
                        break;
                    }
                }
            }
        } while (MovedState);
        if (movement.HorizontalOutput != 0)
        {
            renderer.flipX = movement.HorizontalOutput < 0;
            flipParent.rotation = Quaternion.Euler(0, movement.HorizontalOutput < 0 ? 180 : 0, 0);
        }
    }
}
