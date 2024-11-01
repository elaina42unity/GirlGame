using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideStateAsh : PlayerStateAsh
{
    public PlayerWallSlideStateAsh(PlayerAsh player, PlayerStateMachineAsh stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine_.ChangeState(player_.WallJumpState);
            return;
        }

        if (xInput_ != 0 && player_.facingDir != xInput_)
            stateMachine_.ChangeState(player_.IdleState);

        if (yInput_ < 0)
            rb_.velocity = new Vector2(0, rb_.velocity.y);
        else
            rb_.velocity = new Vector2(0, rb_.velocity.y * .7f);

        if (player_.IsGroundDetected())
            stateMachine_.ChangeState(player_.IdleState);
    }
}
