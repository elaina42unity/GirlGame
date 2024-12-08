using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashStateAX : PlayerStateAX
{
    public PlayerDashStateAX(PlayerAX _player, PlayerStateMachineAX _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    //initiate
    public override void Enter()
    {
        base.Enter();

        stateTimer = player.dashDuration;
    }

    //set player velocity back
    public override void Exit()
    {
        base.Exit();

        player.SetVelocity(0,rb.velocity.y);
    }


    public override void Update()
    {
        base.Update();

        //if during the dash get the wall detected update state
        if (!player.IsGroundDetected() && player.IsWallDetected())
            stateMachine.ChangeState(player.wallSlide);

        //set the dash velocity
        player.SetVelocity(player.dashSpeed * player.dashDir, 0);  

        //finish dash
        if ((stateTimer<0))
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
