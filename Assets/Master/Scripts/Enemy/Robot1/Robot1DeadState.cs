using System.Collections;
using System.Collections.Generic;
using UnityEditor.AnimatedValues;
using UnityEngine;

public class Robot1DeadState : EnemyState
{
    private EnemyRobot1 enemy;
    public Robot1DeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyRobot1 _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = 3f;

        enemy.anim.SetBool("Die", true);
        enemy.anim.speed = 0;
        enemy.cd.enabled = false;

        stateTimer = -.1f;
    }

    public override void Update()
    {
        base.Update();

        stateTimer -= Time.deltaTime;

        if (stateTimer > 0)
            rb.velocity = new Vector2(0, 10);

        enemy.isDead = true;
    }

}
