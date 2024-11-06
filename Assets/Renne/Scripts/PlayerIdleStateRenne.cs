using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleStateRenne : PlayerGroundedStateRenne
{
    public PlayerIdleStateRenne(PlayerRenne _player, PlayerStateMachineRenne _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (xInput != 0)
        {
            stateMachine.ChangesState(player.moveState);
        }
    }
}
