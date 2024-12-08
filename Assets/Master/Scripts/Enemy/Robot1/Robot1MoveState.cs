using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot1MoveState : Robot1GroundedState
{
    public Robot1MoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyRobot1 _enemy) : base(_enemyBase, _stateMachine, _animBoolName, _enemy)
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

        enemy.SetVelocity(enemy.movespeed * enemy.facingDir, rb.velocity.y);

        if (enemy.IsWallDetected() || !enemy.IsGroundDetected())
        {
            enemy.Flip();
            stateMachine.ChangeState(enemy.idleState);
        }
    }
}
