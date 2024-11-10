using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpStateRenne : PlayerStateRenne
{
    public PlayerJumpStateRenne(PlayerRenne _player, PlayerStateMachineRenne _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        rb.velocity = new Vector2(rb.velocity.x, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (rb.velocity.y < 0)
        {
            stateMachine.ChangesState(player.airState);
        }
    }
}
