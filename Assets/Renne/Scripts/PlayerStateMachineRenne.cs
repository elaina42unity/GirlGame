using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachineRenne
{
    public PlayerStateRenne currentState { get; private set; }

    public void Initialize(PlayerStateRenne _starState)
    {
        currentState = _starState;
        currentState.Enter();
    }

    public void ChangesState(PlayerStateRenne _newState)
    {
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }

}
