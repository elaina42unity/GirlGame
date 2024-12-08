using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpStateAX : PlayerStateAX
{
    public PlayerWallJumpStateAX(PlayerAX _player, PlayerStateMachineAX _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //reset the timer
        stateTimer = 1f;

        //set jump
        player.SetVelocity(5 * -player.facingDir, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        //check the time can be wallslied
        if(stateTimer < 0)
        {
            stateMachine.ChangeState(player.airState);  
        }

        //updater to grounde state
        if(player.IsGroundDetected())
            stateMachine.ChangeState(player.idleState);
    }
}
