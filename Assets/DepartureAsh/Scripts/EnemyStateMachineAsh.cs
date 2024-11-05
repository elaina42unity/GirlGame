using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachineAsh 
{
    public EnemyStateAsh CurrentState { get; private set; }

    public void Initialize(EnemyStateAsh startState)
    {
        CurrentState = startState;
        CurrentState.Enter();
    }

    public void ChangeState(EnemyStateAsh newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
