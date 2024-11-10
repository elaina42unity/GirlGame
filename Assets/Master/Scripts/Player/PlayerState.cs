using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerState : State
{

    protected float XInput { get; private set; }
    protected float YInput { get; private set; }

    public Player PlayerObject
    {
        get { return base.Entity as Player; }
        protected set { base.Entity = value; }
    }

    public PlayerState(Entity entity, StateMachine stateMachine, string animBoolName) : base(entity, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        base.Update();
        XInput = Input.GetAxisRaw("Horizontal");
    }
    public override void Exit()
    {
        base.Exit();
    }

    public override void CopyInfoFromOtherState(State otherState)
    {
        if (null==otherState)
            Debug.LogError("Other state is null");
        else if(otherState is not PlayerState)
            Debug.LogError("Other state is not PlayerState");

        PlayerState otherPlayerState = otherState as PlayerState;
        XInput = otherPlayerState.XInput;
    }
}
