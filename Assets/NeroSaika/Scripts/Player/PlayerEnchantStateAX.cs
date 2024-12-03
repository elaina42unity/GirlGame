using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnchantStateAX : PlayerStateAX
{
    public PlayerEnchantStateAX(PlayerAX _player, PlayerStateMachineAX _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

        stateMachine.ChangeState(player.idleState);
       
    }
}
