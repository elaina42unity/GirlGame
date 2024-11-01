using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// super state.super state is something that can control transition between simple state.
/// as a enample,GroundedState is super state of IdleState and MoveState.
/// </summary>
public class PlayerGroundedStateAsh : PlayerStateAsh
{
    public PlayerGroundedStateAsh(PlayerAsh player, PlayerStateMachineAsh stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
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

        if (Input.GetKeyDown(KeyCode.Mouse0))
            stateMachine_.ChangeState(player_.PrimaryAttackState);

        if (!player_.IsGroundDetected())
            stateMachine_.ChangeState(player_.AirState);

        if (Input.GetKeyDown(KeyCode.Space)&&player_.IsGroundDetected())
            stateMachine_.ChangeState(player_.JumpState); 
    }
}
