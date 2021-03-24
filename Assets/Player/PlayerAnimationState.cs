using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public struct PlayerAnimationStateTransition
{
    public System.Func<PlayerAnimationState> To;
    public System.Func<bool> Condition;
}

public class PlayerAnimationStateEvent : UnityEvent { }

[System.Serializable]
public class PlayerAnimationState
{
    public string StateName;
    public bool Loop;
    public List<PlayerAnimationStateTransition> ImmediateTransitions;
    public List<PlayerAnimationStateTransition> EndTransitions;
    public PlayerAnimationStateEvent OnStateEnter = new PlayerAnimationStateEvent();
    public PlayerAnimationStateEvent OnStateExit = new PlayerAnimationStateEvent();
    public void Play(Animator animator)
    {
        animator.Play(StateName);
    }

    public PlayerAnimationState(string stateName, bool loop, List<PlayerAnimationStateTransition> immediateTransitions = null, List<PlayerAnimationStateTransition> endTransitions = null)
    {
        StateName = stateName;
        Loop = loop;
        ImmediateTransitions = immediateTransitions;
        EndTransitions = endTransitions;
    }
}