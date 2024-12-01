using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideStateAX : PlayerStateAX
{
    public PlayerWallSlideStateAX(PlayerAX _player, PlayerStateMachineAX _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

        if (xInput != 0 && player.facingDir != xInput)
            stateMachine.ChangeState(player.idleState);

        if (yInput < 0)
            rb.velocity = new Vector2(0, rb.velocity.y);
        else rb.velocity = new Vector2(0, rb.velocity.y * .7f);

        rb.velocity=new Vector2(0,rb.velocity.y);

        if(player.IsGroundDetected())
            stateMachine.ChangeState(player.idleState);


    }
}
