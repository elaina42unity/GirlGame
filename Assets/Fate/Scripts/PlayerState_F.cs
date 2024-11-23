using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_F
{
    protected PlayerStateMachine_F stateMachine;
    protected Player_F player;

    protected Rigidbody2D rb;

    protected float xInput;
    private string animBoolName;

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
    }

    public virtual void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        player.anim.SetFloat("yVelocity", rb.velocity.y);

    }

    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);

    }


}
