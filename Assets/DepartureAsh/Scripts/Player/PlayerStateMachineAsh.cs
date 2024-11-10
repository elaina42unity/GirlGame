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
        // TODO:BUG 需要将上一个状态的基本信息复制给新的状态（如输入信息），因为新的状态是和旧的状态不同的实例，两者的数据不能共享，如果需要在新的状态的enter中用到上一个状态中update更新后的数据会出现数 据丢失的bug
        newState.xInput_ = CurrentState.xInput_;
        newState.yInput_ = CurrentState.yInput_;
        CurrentState = newState;
        CurrentState.Enter();
    }
}
