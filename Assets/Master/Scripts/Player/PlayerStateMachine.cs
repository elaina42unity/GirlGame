using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState CurrentState { get; private set; } // Current state of the player

    /// <summary>
    /// Initializes the player state machine with the given start state
    /// </summary>
    /// <param name="startState"> The start state of the player </param>
    public void Initialize(PlayerState startState)
    {
        // Sets the current state to the start state 
        CurrentState = startState;
        // Enters the start state
        CurrentState.Enter();
    }

    /// <summary>
    /// Changes the player's state to the given state
    /// </summary>
    /// <param name="newState"> The new state of the player </param>
    public void ChangeState(PlayerState newState)
    {
        // Exits the current state
        CurrentState.Exit();
        // Sets the current state to the new state
        CurrentState = newState;
        // Enters the new state
        CurrentState.Enter();
    }
}
