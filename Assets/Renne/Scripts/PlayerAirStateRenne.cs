using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirStateRenne : PlayerStateRenne
{
    public PlayerAirStateRenne(PlayerRenne _player, PlayerStateMachineRenne _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

        if (player.IsWallDetected())
        {
            stateMachine.ChangesState(player.wallSlideState);
        }

        if (player.IsGroundDetected())
        {
            stateMachine.ChangesState(player.idleState);
        }

        if (xInput != 0)
        {
            player.SetVelocity(player.moveSpeed * 0.8f * xInput, rb.velocity.y);
        }
    }
}
