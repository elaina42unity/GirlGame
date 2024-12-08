using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_SkeletonFate : EnemyFate
{
    #region States
    public SkeletonIdleStateFate idleState { get; private set; }
    public SkeletonMoveStateFate moveState { get; private set; }
    public SkeletonBattleStateFate battleState { get; private set; }    
    public SkeletonAttackStateFate attackState { get; private set; }    
    #endregion


    protected override void Awake()
    {
        base.Awake();

        idleState = new SkeletonIdleStateFate(this, stateMachine, "Idle", this);
        moveState = new SkeletonMoveStateFate(this, stateMachine, "Move", this);
        battleState = new SkeletonBattleStateFate(this, stateMachine, "Move", this);
        attackState = new SkeletonAttackStateFate(this, stateMachine, "Attack", this);


    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }
}
