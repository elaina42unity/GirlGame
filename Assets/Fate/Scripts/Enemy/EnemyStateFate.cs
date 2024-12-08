using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateFate
{

    protected EnemyStateMachineFate stateMachine;
    protected EnemyFate enemyBase;
    protected Rigidbody2D rb;

    private string animBoolName;

    protected float stateTimer;
    protected bool triggerCalled;


    public EnemyStateFate(EnemyFate _enemyBase, EnemyStateMachineFate _stateMachine, string _animaBpplName)
    {
        this.enemyBase = _enemyBase;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animaBpplName;

    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }


    public virtual void Enter()
    {
        triggerCalled = false;
        rb = enemyBase.rb;
        enemyBase.anim.SetBool(animBoolName, true);
    }

    public virtual void Exit()
    {
        enemyBase.anim.SetBool(animBoolName, false);


    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }


}
