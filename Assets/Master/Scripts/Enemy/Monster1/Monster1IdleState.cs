using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster1IdleState : Monster1GroundedState
{
    public Monster1IdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyMonster1 _enemy) : base(_enemyBase, _stateMachine, _animBoolName, _enemy)
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
