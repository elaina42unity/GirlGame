using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttackStateAsh : EnemyStateAsh
{
    private Enemy_SkeletonAsh enemy_;

    public SkeletonAttackStateAsh(EnemyAsh enemyBase, EnemyStateMachineAsh stateMachine, string animBoolName, Enemy_SkeletonAsh enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        enemy_ = enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        enemy_.lastTimeAttacked_ = Time.time;
    }

    public override void Update()
    {
        base.Update();

        enemy_.SetZeroVelocity();

        if (triggerCalled_)
            stateMachine_.ChangeState(enemy_.BattleState);
    }
}
