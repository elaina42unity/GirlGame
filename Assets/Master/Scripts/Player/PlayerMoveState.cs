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

        PlayerObject.SetVelocityWithFlipCheck(XInput * PlayerObject.moveSpeed_, rb_.velocity.y);

        // if player is not moving or player is touching wall then change the state to idle
        if (XInput == 0 || PlayerObject.IsWallDetected()) 
            stateMachine_.ChangeState(PlayerObject.IdleState);
    }
}
