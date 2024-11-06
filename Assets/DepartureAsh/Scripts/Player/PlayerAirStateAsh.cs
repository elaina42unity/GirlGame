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

        if (player_.IsWallDetected())
            stateMachine_.ChangeState(player_.WallSlideState);

        if (player_.IsGroundDetected())
            stateMachine_.ChangeState(player_.IdleState);

        if (xInput_!=0)
            player_.SetVelocity(player_.moveSpeed_ *.8f* xInput_, rb_.velocity.y);
    }
}
