using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMoveStateAsh : SkeletonGroundedStateAsh
{
    public SkeletonMoveStateAsh(EnemyAsh enemyBase, EnemyStateMachineAsh stateMachine, string animBoolName, Enemy_SkeletonAsh enemy) : base(enemyBase, stateMachine, animBoolName, enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        enemy_.SetVelocity(enemy_.moveSpeed_ * enemy_.FacingDir, rb_.velocity.y);

        if (enemy_.IsWallDetected()||!enemy_.IsGroundDetected())
        {
            enemy_.Flip();
            stateMachine_.ChangeState(enemy_.IdleState);
        }
    }
}
