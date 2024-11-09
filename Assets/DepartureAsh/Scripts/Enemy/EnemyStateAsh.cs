using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateAsh
{
    protected EnemyStateMachineAsh stateMachine_;
    protected EnemyAsh enemyBase_;
    protected Rigidbody2D rb_;

    protected bool triggerCalled_;
    private string animBoolName_;

    protected float stateTimer_;

    public EnemyStateAsh(EnemyAsh enemyBase, EnemyStateMachineAsh stateMachine, string animBoolName)
    {
        stateMachine_ = stateMachine;
        enemyBase_ = enemyBase;
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
        rb_ = enemyBase_.Rb;
        enemyBase_.Anim.SetBool(animBoolName_,true);
    }

    public virtual void Exit()
    {
        enemyBase_.Anim.SetBool(animBoolName_, false);
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled_ = true;
    }
}
