using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonIdleStateFate : SkeletonGroundStateFate
{
    public SkeletonIdleStateFate(EnemyFate _enemyBase, EnemyStateMachineFate _stateMachine, System.String _animaBpplName, Enemy_SkeletonFate _enemy) : base(_enemyBase, _stateMachine, _animaBpplName, _enemy)
    {

    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = enemy.idleTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
        {
            stateMachine.ChangeStage(enemy.moveState);
        }

    }
}
