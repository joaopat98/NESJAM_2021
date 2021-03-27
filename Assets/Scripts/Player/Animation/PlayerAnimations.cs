using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] Transform flipParent;
    PlayerEntity player;
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

    [HideInInspector] public PlayerAnimationState Idle, Run, BusterPrepare, BusterShoot, Jump, Fall, GetHit;
    public bool PrepareBuster;
    public PlayerAnimationState current;
    // Start is called before the first frame update
    void Awake()
    {
        player = GetComponent<PlayerEntity>();
        anim = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();

        PlayerAnimationStateTransition JumpTransition = new PlayerAnimationStateTransition
        {
            To = () => Jump,
            Condition = () => !player.controller.isGrounded
        };

        PlayerAnimationStateTransition BusterTransition = new PlayerAnimationStateTransition
        {
            To = () => BusterPrepare,
            Condition = () => PrepareBuster
        };

        PlayerAnimationStateTransition GetHitTransition = new PlayerAnimationStateTransition
        {
            To = () => GetHit,
            Condition = () => player.health.GettingHit
        };

        Idle = new PlayerAnimationState
        {
            StateName = "Idle",
            Loop = true,
            ImmediateTransitions = new List<PlayerAnimationStateTransition> {
                GetHitTransition,
                new PlayerAnimationStateTransition {
                    To = () => Run,
                    Condition = () => player.movement.HorizontalOutput != 0
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
                GetHitTransition,
                new PlayerAnimationStateTransition {
                    To = () => Idle,
                    Condition = () => player.movement.HorizontalOutput == 0
                },
                BusterTransition,
                JumpTransition,
            }
        };

        BusterPrepare = new PlayerAnimationState
        {
            StateName = "Buster Prepare",
            Loop = false,
            ImmediateTransitions = new List<PlayerAnimationStateTransition> {
                GetHitTransition,
                new PlayerAnimationStateTransition {
                    To = () => BusterShoot,
                    Condition = () => AnimationFinished && !PrepareBuster
                }
            }
        };

        BusterShoot = new PlayerAnimationState
        {
            StateName = "Buster Shoot",
            Loop = false,
            ImmediateTransitions = new List<PlayerAnimationStateTransition> {
                GetHitTransition,
                new PlayerAnimationStateTransition {
                    To = () => Run,
                    Condition = () => AnimationFinished && ActiveInputThisFrame
                },
                new PlayerAnimationStateTransition {
                    To = () => Jump,
                    Condition = () => !player.controller.isGrounded && player.movement.input.StartJump
                },
                BusterTransition,
            }
        };

        Jump = new PlayerAnimationState
        {
            StateName = "Jump",
            Loop = true,
            ImmediateTransitions = new List<PlayerAnimationStateTransition> {
                GetHitTransition,
                new PlayerAnimationStateTransition {
                    To = () => Idle,
                    Condition = () => player. controller.isGrounded
                },
                BusterTransition,
                new PlayerAnimationStateTransition {
                    To = () => Fall,
                    Condition = () => player.controller.velocity.y < 0
                },
            }
        };

        Fall = new PlayerAnimationState
        {
            StateName = "Fall",
            Loop = true,
            ImmediateTransitions = new List<PlayerAnimationStateTransition> {
                GetHitTransition,
                new PlayerAnimationStateTransition {
                    To = () => Idle,
                    Condition = () => player.controller.isGrounded
                },
                BusterTransition,
            }
        };

        GetHit = new PlayerAnimationState
        {
            StateName = "Get Hit",
            Loop = true,
            ImmediateTransitions = new List<PlayerAnimationStateTransition>
            {
                new PlayerAnimationStateTransition {
                    To = () => Idle,
                    Condition = () => !player.health.GettingHit
                }
            }
        };

        current = null;
        MoveToState(Idle);
    }

    void Start()
    {

    }

    void MoveToState(PlayerAnimationState state)
    {
        if (current != null && current != state)
            current.OnStateExit.Invoke();
        current = state;
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
        if (player.movement.HorizontalOutput != 0)
        {
            renderer.flipX = player.movement.HorizontalOutput < 0;
            flipParent.rotation = Quaternion.Euler(0, player.movement.HorizontalOutput < 0 ? 180 : 0, 0);
        }
    }
}
