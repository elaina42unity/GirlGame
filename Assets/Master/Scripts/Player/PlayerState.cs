using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState 
{
    protected PlayerStateMachine stateMachine_;
    protected Player player_;

    protected Rigidbody2D rb_;

    protected float xInput_;
    protected float yInput_;
    private string animBoolName_;

    protected float stateTimer_;
    protected bool triggerCalled_;

    public PlayerState(Player player, PlayerStateMachine stateMachine, string animBoolName)
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
