using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateAX
{
    protected PlayerStateMachineAX stateMachine;
    protected PlayerAX player;

    protected Rigidbody2D rb;

    protected float xInput;
    protected float yInput;

    private string animBoolName;

    protected float stateTimer;

    // Start is called before the first frame update
    public PlayerStateAX(PlayerAX _player,PlayerStateMachineAX _stateMachine,string _animBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    // Enter is called only once 
    public virtual void Enter()
    {
        player.anim.SetBool(animBoolName, true);
        rb = player.rb;
    }
    // Update is called once per frame
    public virtual void Update()
    {
        stateTimer -= Time.deltaTime; 

        xInput=Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");


        player.anim.SetFloat("yVelocity",rb.velocity.y);

    }
    // Exit is called only once
    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);

    }
}
