using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChantStateAX : PlayerStateAX
{
    public PlayerChantStateAX(PlayerAX _player, PlayerStateMachineAX _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.chantDuration;
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void Update()
    {
        base.Update();
        


        if ((stateTimer<0) || Input.GetKeyDown(KeyCode.X))
        {
            stateMachine.ChangeState(player.idleState);
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            stateMachine.ChangeState(player.dashState);
        }

    }
}
