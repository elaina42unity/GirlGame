using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStunnedStateAsh : EnemyStateAsh
{
    private Enemy_SkeletonAsh enemy_;

    public SkeletonStunnedStateAsh(EnemyAsh enemyBase, EnemyStateMachineAsh stateMachine, string animBoolName, Enemy_SkeletonAsh enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        enemy_ = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        enemy_.Fx.InvokeRepeating("RedColorBlink",0,0.1f);

        stateTimer_ = enemy_.stunDuration_;

        rb_.velocity=new Vector2(-enemy_.FacingDir * enemy_.stunDirection_.x, enemy_.stunDirection_.y);
    }

    public override void Exit()
    {
        base.Exit();

        enemy_.Fx.Invoke("CancelRedBlink",0);
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer_ < 0)
            stateMachine_.ChangeState(enemy_.IdleState);
    }
}
