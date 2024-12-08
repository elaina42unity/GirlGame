using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttackStateFate : EnemyStateFate
{
    private Enemy_SkeletonFate enemy;
    public SkeletonAttackStateFate(EnemyFate _enemyBase, EnemyStateMachineFate _stateMachine, global::System.String _animaBpplName, Enemy_SkeletonFate enemy) : base(_enemyBase, _stateMachine, _animaBpplName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        enemy.lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();

        enemy.SetZeroVelocity();

        if(triggerCalled)
        {
            stateMachine.ChangeStage(enemy.battleState);
        }
    }
}
