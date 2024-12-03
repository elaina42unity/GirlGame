using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot1BattleStateAX : EnemyStateAX
{

    private Transform player;
    private EnemyRobot1AX enemy;
    private int moveDir;
    public Robot1BattleStateAX(EnemyAX _enemyBase, EnemyStateMachineAX _stateMachine, string _animBoolName, EnemyRobot1AX _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        player = GameObject.Find("Player").transform;
    }
    public override void Update()
    {
        base.Update();

        if (enemy.IsPlayerDetected())
        {
            stateTimer = enemy.battleTime;

            if (enemy.IsPlayerDetected().distance < enemy.attackDistance)
            {
                if (CanAttack())
                    stateMachine.ChangeState(enemy.attackState);
            }
        }else
        {
            if (stateTimer < 0 || Vector2.Distance(player.transform.position, enemy.transform.position) > 40)
                stateMachine.ChangeState(enemy.idleState);
        }

        if (player.position.x > enemy.transform.position.x)
            moveDir = 1;
        else if (player.position.x < enemy.transform.position.x)
            moveDir = -1;

        enemy.SetVelocity(enemy.movespeed * moveDir, rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
    }

    private bool CanAttack()
    {
        if(Time.time >= enemy.lastTimeAttacked + enemy.attackCooldown)
        {
            Debug.Log("133");
            enemy.lastTimeAttacked = Time.time;
            return true;
        }
        return false;
    }

}
