using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMoveStateFate : SkeletonGroundStateFate
{
    public SkeletonMoveStateFate(EnemyFate _enemyBase, EnemyStateMachineFate _stateMachine, System.String _animaBpplName, Enemy_SkeletonFate _enemy) : base(_enemyBase, _stateMachine, _animaBpplName, _enemy)
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

        enemy.SetVelocity(enemy.moveSpeed * enemy.facingDir, rb.velocity.y);

        if (enemy.IsWallDected() || !enemy.IsGroundDected())
        {
            enemy.Flip();
            stateMachine.ChangeStage(enemy.idleState);
        }


    }
}
