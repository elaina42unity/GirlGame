using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateAsh
{
    protected PlayerStateMachineAsh stateMachine_;
    protected PlayerAsh player_;

    protected Rigidbody2D rb_;

    protected float xInput_;
    private string animBoolName_;

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
    }

    public virtual void Update()
    {
        xInput_ = Input.GetAxisRaw("Horizontal");
        
        player_.Anim.SetFloat("yVelocity", rb_.velocity.y); 
    }

    public virtual void Exit()
    {
        player_.Anim.SetBool(animBoolName_, false);

    }

}
