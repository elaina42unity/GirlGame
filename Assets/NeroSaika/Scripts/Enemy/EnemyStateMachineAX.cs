using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachineAX
{

    public EnemyStateAX currentState {  get; private set; }

    public void Initialiaze(EnemyStateAX _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }

    public void ChangeState(EnemyStateAX _newState)
    {
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }
}
