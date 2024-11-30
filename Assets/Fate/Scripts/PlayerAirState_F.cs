using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState_F : PlayerState_F
{
    public PlayerAirState_F(Player_F _player, PlayerStateMachine_F _stateMachine, global::System.String _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

        if (player.IsGroundDected())
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
