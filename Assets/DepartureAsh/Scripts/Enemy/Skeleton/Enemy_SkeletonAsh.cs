using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_SkeletonAsh : EnemyAsh
{
    #region States
    public SkeletonIdleStateAsh IdleState { get; private set; }
    public SkeletonMoveStateAsh MoveState { get; private set; }
    public SkeletonBattleStateAsh BattleState { get; private set; }
    public SkeletonAttackStateAsh AttackState { get; private set; }
    public SkeletonStunnedStateAsh StunnedState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        IdleState = new SkeletonIdleStateAsh(this,StateMachine, "Idle",this);
        MoveState = new SkeletonMoveStateAsh(this, StateMachine, "Move",this);
        BattleState = new SkeletonBattleStateAsh(this, StateMachine, "Move", this);
        AttackState = new SkeletonAttackStateAsh(this, StateMachine, "Attack", this);
        StunnedState = new SkeletonStunnedStateAsh(this, StateMachine, "Stunned", this);
    }

    protected override void Start()
    {
        base.Start();
        StateMachine.Initialize(IdleState);
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.U))
        {
            StateMachine.ChangeState(StunnedState);
        }
    }

    protected override bool CanBeStunned()
    {
        if (base.CanBeStunned())
        {
            StateMachine.ChangeState(StunnedState);
            return true;
        }
        return false;
    }
}
