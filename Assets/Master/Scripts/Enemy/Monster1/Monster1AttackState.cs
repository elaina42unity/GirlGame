using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster1AttackState : EnemyState
{

    private EnemyMonster1 enemy;
    public Monster1AttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyMonster1 _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        enemy.SetZeroVelocity();

        stateTimer = enemy.attackTiming;

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
        
        stateTimer -= Time.deltaTime;

        if(stateTimer > 0)
        {
            enemy.SetZeroVelocity();
        }
        else
        {
            enemy.SetVelocity(50 * enemy.facingDir, rb.velocity.y);
        }


        if(triggerCalled) 
            stateMachine.ChangeState(enemy.battleState);
    }
}
