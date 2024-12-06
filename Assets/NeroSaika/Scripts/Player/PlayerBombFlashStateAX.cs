using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBombFlashStateAX : PlayerStateAX
{
    public PlayerBombFlashStateAX(PlayerAX _player, PlayerStateMachineAX _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.skill.clone.CreateClone(player.transform, new Vector3(0, 0));

        stateTimer = player.bombFlashDuration;
    }

    public override void Exit()
    {
        base.Exit();

        player.Flip();

        player.SetVelocity(0,rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(player.bombFlashSpeed * -player.bombFlashDir, 0);  

        if ((stateTimer<0))
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
