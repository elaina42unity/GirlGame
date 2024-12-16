using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMonster1 : Enemy
{
    [Header("Attack info")]
    public float attackTiming;

    #region States

    public Monster1IdleState idleState {  get; private set; }

    public Monster1MoveState moveState { get; private set; }

    public Monster1BattleState battleState { get; private set; }

    public Monster1AttackState attackState { get; private set; }

    public Monster1StunnedState stunnedState { get; private set; }

    public Monster1DeadState deadState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        idleState = new Monster1IdleState(this, stateMachine, "Idle", this);
        moveState = new Monster1MoveState(this, stateMachine, "Move", this);
        battleState = new Monster1BattleState(this, stateMachine, "Move", this);
        attackState = new Monster1AttackState(this, stateMachine, "Attack", this);
        stunnedState = new Monster1StunnedState(this, stateMachine, "Stunned", this);
        deadState = new Monster1DeadState(this, stateMachine, "Die", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialiaze(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }

    public override bool CanBeStunned()
    {
        if (base.CanBeStunned())
        {
            stateMachine.ChangeState(stunnedState);
            return true;    
        }
        return false;
    }

    public override void Die()
    {
        base.Die();

        stateMachine.ChangeState(deadState);
    }
}
