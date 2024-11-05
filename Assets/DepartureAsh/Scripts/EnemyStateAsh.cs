using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateAsh
{
    protected EnemyStateMachineAsh stateMachine_;
    protected EnemyAsh enemy_;

    protected bool triggerCalled_;
    private string animBoolName_;

    protected float stateTimer_;

    public EnemyStateAsh(EnemyStateMachineAsh stateMachine, EnemyAsh enemy, string animBoolName)
    {
        stateMachine_ = stateMachine;
        enemy_ = enemy;
        animBoolName_ = animBoolName;
    }

    public virtual void Update()
    {
        stateTimer_ -= Time.deltaTime;
        if (stateTimer_ < -10000.0f)
        {
            stateTimer_ = -0.1f;
        }


    }

    public virtual void Enter()
    {
        triggerCalled_ = false;
        enemy_.Anim.SetBool(animBoolName_,true);
    }

    public virtual void Exit()
    {
        enemy_.Anim.SetBool(animBoolName_, false);
    }
}
