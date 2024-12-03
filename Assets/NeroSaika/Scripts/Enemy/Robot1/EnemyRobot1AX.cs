using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRobot1AX : EnemyAX
{
    #region States

    public Robot1IdleStateAX idleState {  get; private set; }

    public Robot1MoveStateAX moveState { get; private set; }

    public Robot1BattleStateAX battleState { get; private set; }

    public Robot1AttackStateAX attackState { get; private set; }

    public Robot1StunnedStateAX stunnedState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        idleState = new Robot1IdleStateAX(this, stateMachine, "Idle", this);
        moveState = new Robot1MoveStateAX(this, stateMachine, "Move", this);
        battleState = new Robot1BattleStateAX(this, stateMachine, "Move", this);
        attackState = new Robot1AttackStateAX(this, stateMachine, "Attack", this);
        stunnedState = new Robot1StunnedStateAX(this, stateMachine, "Stunned", this);
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
}
