using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachineFate
{

    public EnemyStateFate currentState { get; private set; }

    public void Initialize(EnemyStateFate _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }

    public void ChangeStage(EnemyStateFate _newState)
    {
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }
}
