using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateAX
{
    protected PlayerStateMachineAX stateMachine;
    protected PlayerAX player;

    private string animBoolName;

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
    }
    // Update is called once per frame
    public virtual void Update()
    {
        Debug.Log("i am in" + animBoolName);

    }
    // Exit is called only once
    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);

    }
}
