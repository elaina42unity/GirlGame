using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedStateRenne : PlayerStateRenne
{
    public PlayerGroundedStateRenne(PlayerRenne _player, PlayerStateMachineRenne _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

        if(Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangesState(player.jumpState);
        }
    }
}
