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
        // TODO:BUG 需要将上一个状态的基本信息复制给新的状态（如输入信息），因为新的状态是和旧的状态不同的实例，两者的数据不能共享，如果需要在新的状态的enter中用到上一个状态中update更新后的数据会出现数 据丢失的bug
        CurrentState = newState;
        CurrentState.Enter();
    }
}
