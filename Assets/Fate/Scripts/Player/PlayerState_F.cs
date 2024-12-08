using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_F
{
    protected PlayerStateMachine_F stateMachine;
    protected Player_F player;

    protected Rigidbody2D rb;

    protected float xInput;
    protected float yInput;
    private string animBoolName;

    protected float stateTimer;
    protected bool triggerCalled;

    public PlayerState_F(Player_F _player, PlayerStateMachine_F _stateMachine, string _animBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        player.anim.SetBool(animBoolName, true);
        rb = player.rb;
        triggerCalled = false;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;

        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        player.anim.SetFloat("yVelocity", rb.velocity.y);

    }

    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);

    }

    public virtual void AnimationFinishTrigger()
    {

        triggerCalled = true;
    }


}
