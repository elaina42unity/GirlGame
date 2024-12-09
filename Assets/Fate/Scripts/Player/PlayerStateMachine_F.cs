using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine_F : MonoBehaviour
{
    public PlayerState_F currentState { get; private set; }

    public void Initialize(PlayerState_F _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }

    public void ChangeState(PlayerState_F _newState)
    {
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();

    }


}
