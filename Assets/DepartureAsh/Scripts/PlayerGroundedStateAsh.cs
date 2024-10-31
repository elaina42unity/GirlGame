using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        if (Input.GetKeyDown(KeyCode.Space)&&player_.IsGroundDetected())
            stateMachine_.ChangeState(player_.JumpState); 
    }
}
