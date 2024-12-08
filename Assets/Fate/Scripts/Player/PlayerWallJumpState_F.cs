using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState_F : PlayerState_F
{
    public PlayerWallJumpState_F(Player_F _player, PlayerStateMachine_F _stateMachine, global::System.String _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = 0.4f;
        player.SetVelocity(5 * -player.facingDir, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(stateTimer < 0)
        {
            stateMachine.ChangeState(player.airState);
        }

        if(player.IsGroundDected())
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
