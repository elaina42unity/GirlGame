using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimSwordStateAsh : PlayerStateAsh
{
    public PlayerAimSwordStateAsh(PlayerAsh player, PlayerStateMachineAsh stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyUp(KeyCode.L))
            stateMachine_.ChangeState(player_.IdleState);
    }
}
