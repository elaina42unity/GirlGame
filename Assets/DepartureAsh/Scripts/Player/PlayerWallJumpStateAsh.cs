using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpStateAsh : PlayerStateAsh
{
    public PlayerWallJumpStateAsh(PlayerAsh player, PlayerStateMachineAsh stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer_ = 1f;
        player_.SetVelocity(5 * -player_.FacingDir, player_.jumpForce_);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer_ < 0)
            stateMachine_.ChangeState(player_.AirState);

        if (player_.IsWallDetected())
            stateMachine_.ChangeState(player_.WallSlideState);

        if (player_.IsGroundDetected())
            stateMachine_.ChangeState(player_.IdleState);
    }
}
