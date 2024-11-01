using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveStateAsh : PlayerGroundedStateAsh
{
    public PlayerMoveStateAsh(PlayerAsh player, PlayerStateMachineAsh stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
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

        player_.SetVelocity(xInput_*player_.moveSpeed_,rb_.velocity.y);
        if (xInput_ == 0|| player_.IsWallDetected())
        {
            stateMachine_.ChangeState(player_.IdleState);
        }
    }
}
