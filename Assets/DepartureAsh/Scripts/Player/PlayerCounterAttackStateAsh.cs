using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCounterAttackStateAsh : PlayerStateAsh
{
    public PlayerCounterAttackStateAsh(PlayerAsh player, PlayerStateMachineAsh stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer_ = player_.counterAttackDuration_;
        player_.Anim.SetBool("SuccessfulCounterAttack", false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        player_.SetZeroVelocity();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(player_.attackCheck_.position, player_.attackCheckRadius_);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<EnemyAsh>() != null)
            {
                if (hit.GetComponent<EnemyAsh>().CanBeStunned())
                {
                    stateTimer_ = 10.0f; // any value bigger than 1
                    player_.Anim.SetBool("SuccessfulCounterAttack", true);
                }
            }
        }

        if (stateTimer_ < 0 || triggerCalled_)
            stateMachine_.ChangeState(player_.IdleState);
    }
}
