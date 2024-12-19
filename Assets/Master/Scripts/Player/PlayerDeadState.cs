using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerState
{
    public PlayerDeadState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        stateTimer = 8f;

        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        player.isdead = true;

        stateTimer -= Time.deltaTime;

        player.SetZeroVelocity();

        if (stateTimer < 0)
        {

            player.isdead = false;
            stateMachine.ChangeState(player.idleState);
            player.stats.currentHealth = player.stats.maxHealth.GetValue();

        }
    }
}
