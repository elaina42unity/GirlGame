using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonGroundedStateAsh : EnemyStateAsh
{
    protected Enemy_SkeletonAsh enemy_;

    protected Transform player_;

    public SkeletonGroundedStateAsh(EnemyAsh enemyBase, EnemyStateMachineAsh stateMachine, string animBoolName, Enemy_SkeletonAsh enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        enemy_ = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        player_ = GameObject.Find("Player").transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (enemy_.IsPlayerDetected() || Vector2.Distance(enemy_.transform.position, player_.position) < 2)
            stateMachine_.ChangeState(enemy_.BattleState);
    }
}
