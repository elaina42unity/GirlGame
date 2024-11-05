using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerIdleStateAsh : PlayerGroundedStateAsh
{
    public PlayerIdleStateAsh(PlayerAsh player, PlayerStateMachineAsh stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player_.ZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (xInput_ == player_.facingDir && player_.IsWallDetected())
            return;

        if (xInput_!=0&&!player_.IsBusy)
            stateMachine_.ChangeState(player_.MoveState);
    }
}
