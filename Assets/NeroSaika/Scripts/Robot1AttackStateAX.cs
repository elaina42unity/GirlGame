using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot1AttackStateAX : EnemyStateAX
{
    private EnemyRobot1AX enemy;
    public Robot1AttackStateAX(EnemyAX _enemyBase, EnemyStateMachineAX _stateMachine, string _animBoolName, EnemyRobot1AX enemy) : base(_enemyBase, _stateMachine, _animBoolName)
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
            stateMachine.ChangeState(enemy.battleState);
    }
}
