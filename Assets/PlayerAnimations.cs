using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    PlayerMovement movement;
    Animator anim;
    new SpriteRenderer renderer;

    bool AnimationFinished
    {
        get
        {
            return anim.GetCurrentAnimatorStateInfo(0).length <= anim.GetCurrentAnimatorStateInfo(0).normalizedTime
                    || !current.Loop;
        }
    }
    bool MovedState;

    public PlayerAnimationState Idle, Run;
    public PlayerAnimationState current;
    // Start is called before the first frame update
    void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();

        Idle = new PlayerAnimationState(
            "Idle",
            true,
            new List<PlayerAnimationStateTransition> {
                new PlayerAnimationStateTransition {
                    To = () => Run,
                    Condition = () => movement.HorizontalOutput != 0
                },
            }
        );
        Run = new PlayerAnimationState(
            "Run",
            true,
            new List<PlayerAnimationStateTransition> {
                new PlayerAnimationStateTransition {
                    To = () => Idle,
                    Condition = () => movement.HorizontalOutput == 0
                },
            }
        );

        current = null;
        MoveToState(Idle);
    }

    void MoveToState(PlayerAnimationState state)
    {
        if (current != null)
            current.OnStateExit.Invoke();
        current = state;
        current.OnStateEnter.Invoke();
        current.Play(anim);
        MovedState = true;
    }

    void LateUpdate()
    {
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
            if (AnimationFinished && current.EndTransitions != null)
            {
                for (int i = 0; i < current.EndTransitions.Count; i++)
                {
                    var transition = current.EndTransitions[i];
                    if (transition.Condition())
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
        }
    }
}
