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

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            stateMachine.ChangesState(player.primaryAttackState);
        }

        if (!player.IsGroundDetected())
        {
            stateMachine.ChangesState(player.airState);
        }

        if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected())
        {
            stateMachine.ChangesState(player.jumpState);
        }
    }
}
