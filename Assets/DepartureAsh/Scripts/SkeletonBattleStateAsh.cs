using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBattleStateAsh : EnemyStateAsh
{
    private Transform player_;
    private Enemy_Skeleton enemy_;
    private int moveDir_;
    public SkeletonBattleStateAsh(EnemyAsh enemyBase, EnemyStateMachineAsh stateMachine, string animBoolName, Enemy_Skeleton enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        enemy_ = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        
        player_ = GameObject.Find("Player").transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (enemy_.IsPlayerDetected())
        {
            stateTimer_ = enemy_.battleTime_;
            if (enemy_.IsPlayerDetected().distance < enemy_.attackDistance_)
                if (CanAttack())
                {
                    stateMachine_.ChangeState(enemy_.AttackState);
                }
        }
        else
        {
            if (stateTimer_ < 0||Vector2.Distance(player_.transform.position,enemy_.transform.position)>10.0f)
                stateMachine_.ChangeState(enemy_.IdleState);
        }

        if (player_.position.x > enemy_.transform.position.x)
            moveDir_ = 1;
        else if (player_.position.x < enemy_.transform.position.x)
            moveDir_ = -1;

        enemy_.SetVelocity(enemy_.moveSpeed_ * moveDir_, rb_.velocity.y);
    }

    private bool CanAttack()
    {
        if (Time.time>=enemy_.lastTimeAttacked_+enemy_.attackCooldown_)
        {
            enemy_.lastTimeAttacked_ = Time.time;
            return true;
        }

        return false;
    }
}
