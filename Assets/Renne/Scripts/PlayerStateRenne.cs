using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStateRenne 
{
    protected PlayerStateMachineRenne stateMachine;
    protected PlayerRenne player;

    protected Rigidbody2D rb;

    protected float xInput;
    private string animBoolName;

    public PlayerStateRenne(PlayerRenne _player, PlayerStateMachineRenne _stateMachine, string _animBoolName)
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
