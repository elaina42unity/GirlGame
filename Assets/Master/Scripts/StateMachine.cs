using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public abstract class StateMachine 
{
    protected State CurrentState{ get; set; }

    /// <summary>
    /// Initializes the state machine with the given start state
    /// </summary>
    /// <param name="startState"> The start state  </param>
    public virtual void Initialize(State startState)
    {
        // Sets the current state to the start state 
        CurrentState = startState;
        // Enters the start state
        CurrentState.Enter();
    }

    /// <summary>
    /// Changes the state to the given state
    /// </summary>
    /// <param name="newState"> The new state </param>
    public void ChangeState(State newState)
    {
        // Exits the current state
        CurrentState.Exit();
        CopyDataFromOldStateToNewState(CurrentState, newState);
        // Sets the current state to the new state
        CurrentState = newState;
        // Enters the new state
        CurrentState.Enter();
    }

    protected abstract void CopyDataFromOldStateToNewState(State oldState, State newState);

}
