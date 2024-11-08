using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
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

        player_.SetVelocity(xInput_ * player_.moveSpeed_, rb_.velocity.y);

        // if player is not moving or player is touching wall then change the state to idle
        if (xInput_ == 0 || player_.IsWallDetected()) 
            stateMachine_.ChangeState(player_.IdleState);
    }
}
