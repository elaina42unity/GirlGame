using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonIdleStateAsh : SkeletonGroundedStateAsh
{
    public SkeletonIdleStateAsh(EnemyAsh enemyBase, EnemyStateMachineAsh stateMachine, string animBoolName, Enemy_Skeleton enemy) : base(enemyBase, stateMachine, animBoolName, enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer_ = enemy_.idleTime_;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer_ < 0)
            stateMachine_.ChangeState(enemy_.MoveState);
    }
}
