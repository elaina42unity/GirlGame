using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashStateAsh : PlayerStateAsh
{
    public PlayerDashStateAsh(PlayerAsh player, PlayerStateMachineAsh stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player_.dashDuration_;
    }

    public override void Exit()
    {
        base.Exit();

        player_.SetVelocity(0, rb_.velocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (!player_.IsGroundDetected() && player_.IsWallDetected())
            stateMachine_.ChangeState(player_.WallSlideState);

        player_.SetVelocity(player_.dashSpeed_ * player_.DashDir, 0);

        if (stateTimer < 0)
            stateMachine_.ChangeState(player_.IdleState);

    }
       
}
