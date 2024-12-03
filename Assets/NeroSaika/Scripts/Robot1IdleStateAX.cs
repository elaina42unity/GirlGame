using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot1IdleStateAX : Robot1GroundedStateAX
{
    public Robot1IdleStateAX(EnemyAX _enemyBase, EnemyStateMachineAX _stateMachine, string _animBoolName, EnemyRobot1AX _enemy) : base(_enemyBase, _stateMachine, _animBoolName, _enemy)
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
            stateMachine.ChangeState(enemy.moveState);
    }
}
