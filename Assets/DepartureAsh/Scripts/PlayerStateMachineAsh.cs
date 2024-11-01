using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachineAsh 
{
    public PlayerStateAsh CurrentState { get; private set; }

    public void Initialize(PlayerStateAsh startState)
    {
        CurrentState = startState;
        CurrentState.Enter(); 
    }

    public void ChangeState(PlayerStateAsh newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
