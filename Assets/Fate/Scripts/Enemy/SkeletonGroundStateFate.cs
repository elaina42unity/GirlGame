using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonGroundStateFate : EnemyStateFate
{
    protected Enemy_SkeletonFate enemy;

    protected Transform player;

    public SkeletonGroundStateFate(EnemyFate _enemyBase, EnemyStateMachineFate _stateMachine, global::System.String _animaBpplName, Enemy_SkeletonFate _enemy) : base(_enemyBase, _stateMachine, _animaBpplName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        player = GameObject.Find("Player").transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(enemy.IsPlayerDetected() || Vector2.Distance(enemy.transform.position, player.position) <2 )
        {
            stateMachine.ChangeStage(enemy.battleState);
        }
    }
}
