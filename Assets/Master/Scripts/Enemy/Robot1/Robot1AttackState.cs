using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot1AttackState : EnemyState
{
    private EnemyRobot1 enemy;
    public Robot1AttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyRobot1 _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
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
