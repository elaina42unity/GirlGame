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

        rb_.velocity= new Vector2(0, 0);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (xInput_!=0)
            stateMachine_.ChangeState(player_.MoveState);
    }
}
