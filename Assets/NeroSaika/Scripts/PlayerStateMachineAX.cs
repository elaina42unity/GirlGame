using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachineAX 
{
    public PlayerStateAX currentState { get; private set; }

    public void Initialize(PlayerStateAX _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }

    public void ChangeState(PlayerStateAX _newState)
    {
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }
}
