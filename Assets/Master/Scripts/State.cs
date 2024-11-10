using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.Windows;

public abstract class State 
{
    protected StateMachine stateMachine_;
    protected Entity Entity { get;  set; }

    protected Rigidbody2D rb_;

    protected string animBoolName_;

    protected float stateTimer_;

    protected bool triggerCalled_;

    public State(Entity entity, StateMachine stateMachine, string animBoolName)
    {
        Entity = entity;
        stateMachine_ = stateMachine;
        animBoolName_ = animBoolName;
    }

    public virtual void Enter()
    {
        Entity.Anim.SetBool(animBoolName_, true);
        rb_ = Entity.Rb;
        triggerCalled_ = false;
    }

    public virtual void Update()
    {
        stateTimer_ -= Time.deltaTime;
        if (stateTimer_ < -10000.0f)
        {
            stateTimer_ = -0.1f;
        }
    }

    public virtual void Exit()
    {
        Entity.Anim.SetBool(animBoolName_, false);
    }

    public void AnimationFinishTrigger()
    {
        triggerCalled_ = true;
    }

    public abstract void CopyInfoFromOtherState(State otherState);
}
