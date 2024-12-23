using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster1DeadState : EnemyState
{
    private EnemyMonster1 enemy;
    public Monster1DeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyMonster1 _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        //stateTimer = 5f;

        enemy.anim.SetBool("Die", true);
        //enemy.anim.speed = 0;
        rb.velocity = new Vector2(0, 10);

        //stateTimer = -.1f;
    }

    public override void Update()
    {
        base.Update();

        stateTimer -= Time.deltaTime;

        //if (stateTimer < 0)
        //{
        //    enemy.cd.enabled = false;   
        //}

        enemy.isDead = true;
    }

}
