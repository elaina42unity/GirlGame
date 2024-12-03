using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot1MoveStateAX : Robot1GroundedStateAX
{
    public Robot1MoveStateAX(EnemyAX _enemyBase, EnemyStateMachineAX _stateMachine, string _animBoolName, EnemyRobot1AX _enemy) : base(_enemyBase, _stateMachine, _animBoolName, _enemy)
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
