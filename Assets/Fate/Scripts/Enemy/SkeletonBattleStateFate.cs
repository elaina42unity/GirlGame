using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBattleStateFate : EnemyStateFate
{
    private Transform player;
    private Enemy_SkeletonFate enemy;
    private int moveDir;


    public SkeletonBattleStateFate(EnemyFate _enemyBase, EnemyStateMachineFate _stateMachine, global::System.String _animaBpplName, Enemy_SkeletonFate _enemy) : base(_enemyBase, _stateMachine, _animaBpplName)
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
                {
                    stateMachine.ChangeStage(enemy.attackState);
                }
            }
        }
        else
        {
            if(stateTimer < 0 || Vector2.Distance(player.transform.position,enemy.transform.position) > 15)
            {
                stateMachine.ChangeStage(enemy.idleState);
            }
        }

        if (player.position.x > enemy.transform.position.x)
        {
            moveDir = 1;
        }
        else if (player.position.x < enemy.transform.position.x)
        {
            moveDir = -1;
        }

        enemy.SetVelocity(enemy.moveSpeed * moveDir, rb.velocity.y);


    }

    public override void Exit()
    {
        base.Exit();
    }

    private bool CanAttack()
    {
        if (Time.time >= enemy.lastTimeAttacked + enemy.attackCooldown)
        {
            enemy.lastTimeAttacked = Time.time;
            return true;
        }

        return false;
    }


}
