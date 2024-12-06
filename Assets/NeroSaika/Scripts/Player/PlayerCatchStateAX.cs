using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCatchStateAX : PlayerStateAX
{
    private Transform waterBall;

    public PlayerCatchStateAX(PlayerAX _player, PlayerStateMachineAX _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        waterBall = player.waterBall.transform;

        if (player.transform.position.x > waterBall.position.x && player.facingDir == 1)
            player.Flip();
        else if (player.transform.position.x < waterBall.position.x && player.facingDir == -1)
            player.Flip();

        rb.velocity = new Vector2(player.waterBallReturnImpact * -player.facingDir, rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();

        player.StartCoroutine("BusyFor", .1f);

    }

    public override void Update()
    {
        base.Update();
        if(triggerCalled) 
            stateMachine.ChangeState(player.idleState);
    }
}
