using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirStateAsh : PlayerStateAsh
{
    public PlayerAirStateAsh(PlayerAsh player, PlayerStateMachineAsh stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
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

        if (player_.IsGroundDetected())
            stateMachine_.ChangeState(player_.IdleState);
    }
}
