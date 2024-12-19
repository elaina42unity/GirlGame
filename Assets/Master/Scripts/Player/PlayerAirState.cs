using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

        //wallcheck
        //if (player.IsWallDetected())
        //    stateMachine.ChangeState(player.wallSlide);

        //Ground check
        if (player.IsGroundDetected() || player.IsHalfGroundDetected())
            stateMachine.ChangeState(player.idleState);

        //can move in the sky
        if (xInput != 0)
            player.SetVelocity(player.moveSpeed * .8f * xInput, rb.velocity.y);
    }
}
