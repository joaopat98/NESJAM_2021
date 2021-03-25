using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimationStateTransition
{
    public System.Func<PlayerAnimationState> To = null;
    public System.Func<bool> Condition = null;
}

[System.Serializable]
public class PlayerAnimationStateEvent : UnityEvent { }

[System.Serializable]
public class PlayerAnimationState
{
    public string StateName;
    public bool Loop;
    public List<PlayerAnimationStateTransition> ImmediateTransitions = null;
    public List<PlayerAnimationStateTransition> EndTransitions = null;
    public PlayerAnimationStateEvent OnStateEnter = new PlayerAnimationStateEvent();
    public PlayerAnimationStateEvent OnStateExit = new PlayerAnimationStateEvent();
    public void Play(Animator animator)
    {
        animator.Play(StateName, 0, 0);
    }
}