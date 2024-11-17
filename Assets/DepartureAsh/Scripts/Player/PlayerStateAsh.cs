using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateAsh
{
    protected PlayerStateMachineAsh stateMachine_;
    protected PlayerAsh player_;

    protected Rigidbody2D rb_;

    public float xInput_;
    public float yInput_;
    private string animBoolName_;

    protected float stateTimer_;
    protected bool triggerCalled_;

    public PlayerStateAsh(PlayerAsh player, PlayerStateMachineAsh stateMachine, string animBoolName)
    {
        player_ = player;
        stateMachine_ = stateMachine;
        animBoolName_ = animBoolName;
    }

    public virtual void Enter()
    {

        player_.Anim.SetBool(animBoolName_, true);
        rb_ = player_.Rb;
        triggerCalled_ = false;
    }

    public virtual void Update()
    {
        stateTimer_ -= Time.deltaTime;
        if (stateTimer_ < -10000.0f)
        {
            stateTimer_ = -0.1f;
        }
        xInput_ = Input.GetAxisRaw("Horizontal");
        yInput_ = Input.GetAxisRaw("Vertical");

        player_.Anim.SetFloat("yVelocity", rb_.velocity.y);
    }

    public virtual void Exit()
    {
        player_.Anim.SetBool(animBoolName_, false);
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled_ = true;
    }
}
