using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRobot1 : Enemy
{
    #region States

    public Robot1IdleState idleState {  get; private set; }

    public Robot1MoveState moveState { get; private set; }

    public Robot1BattleState battleState { get; private set; }

    public Robot1AttackState attackState { get; private set; }

    public Robot1StunnedState stunnedState { get; private set; }

    public Robot1DeadState deadState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        idleState = new Robot1IdleState(this, stateMachine, "Idle", this);
        moveState = new Robot1MoveState(this, stateMachine, "Move", this);
        battleState = new Robot1BattleState(this, stateMachine, "Move", this);
        attackState = new Robot1AttackState(this, stateMachine, "Attack", this);
        stunnedState = new Robot1StunnedState(this, stateMachine, "Stunned", this);
        deadState = new Robot1DeadState(this, stateMachine, "Die", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialiaze(idleState);
    }

    protected override void Update()
    {
        base.Update();

        if(Input.GetKeyDown(KeyCode.U))
        {
            stateMachine.ChangeState(stunnedState);
        }
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
