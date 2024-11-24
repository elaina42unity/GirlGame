using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState_F : PlayerGroundedState_F
{
    public PlayerIdleState_F(Player_F _player, PlayerStateMachine_F _stateMachine, global::System.String _animBoolName) : base(_player, _stateMachine, _animBoolName)
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
            stateMachine.ChangeState(player.moveState);
        }
    }
}
