using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public new PlayerState CurrentState
    {
        get { return base.CurrentState as PlayerState; }
        protected set { base.CurrentState = value; }
    }

    protected override void CopyDataFromOldStateToNewState(State oldState, State newState)
    {
        if (null == oldState || null == newState)
        {
            Debug.LogError("Old or new state is null");
            return;
        }
        else if(oldState is not PlayerState || newState is not PlayerState)
        {
            Debug.Log("Old or new state is not a player state");
            return;
        }
        PlayerState oldPlayerState = oldState as PlayerState;
        PlayerState newPlayerState = newState as PlayerState;
        newPlayerState.CopyInfoFromOtherState(oldPlayerState);
    }
}
