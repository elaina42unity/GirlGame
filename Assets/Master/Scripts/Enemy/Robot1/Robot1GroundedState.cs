using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot1GroundedState : EnemyState
{
    protected Transform player;

    protected EnemyRobot1 enemy;
    public Robot1GroundedState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyRobot1 _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        player = PlayerManager.instance.player.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (enemy.IsPlayerDetected() || Vector2.Distance(enemy.transform.position, player.position) < 8)
            stateMachine.ChangeState(enemy.battleState);
    }
}
