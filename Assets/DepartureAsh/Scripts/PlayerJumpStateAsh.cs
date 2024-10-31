using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpStateAsh : PlayerStateAsh
{
    public PlayerJumpStateAsh(PlayerAsh player, PlayerStateMachineAsh stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        rb_.velocity = new Vector2(rb_.velocity.x, player_.jumpForce_);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (rb_.velocity.y < 0)
            stateMachine_.ChangeState(player_.AirState);
    }
}
