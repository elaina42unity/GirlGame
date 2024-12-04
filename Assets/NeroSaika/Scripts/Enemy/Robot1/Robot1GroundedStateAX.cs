using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot1GroundedStateAX : EnemyStateAX
{
    protected Transform player;

    protected EnemyRobot1AX enemy;
    public Robot1GroundedStateAX(EnemyAX _enemyBase, EnemyStateMachineAX _stateMachine, string _animBoolName, EnemyRobot1AX _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        player = PlayerManagerAX.instance.player.transform;
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
