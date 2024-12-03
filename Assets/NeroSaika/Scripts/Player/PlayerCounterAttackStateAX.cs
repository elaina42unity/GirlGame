using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCounterAttackStateAX : PlayerStateAX
{
    public PlayerCounterAttackStateAX(PlayerAX _player, PlayerStateMachineAX _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.counterAttackDuration;
        player.anim.SetBool("SuccessfulCounterAttack", false);

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        Collider2D[] colliders = Physics2D.OverlapBoxAll(player.attackCheck.position, new Vector2(player.attackCheckHeight, player.attackCheckWidth), 0);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<EnemyAX>() != null)
                if (hit.GetComponent<EnemyAX>().CanBeStunned())
                {
                    stateTimer = 10;//any value bigger than 1
                    player.anim.SetBool("SuccessfulCounterAttack", true);
                }
        }

        if(stateTimer < .5f || triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }
}
