using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChantStateAX : PlayerStateAX
{
    int comboCounter;
    float lastTimeAttacked;

    public PlayerChantStateAX(PlayerAX _player, PlayerStateMachineAX _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        

        stateTimer = player.chantDuration;

        if (comboCounter > 1 )
            comboCounter = 0;

        player.anim.SetInteger("ComboCounter", comboCounter);
    }

    public override void Exit()
    {
        base.Exit();
        comboCounter++;

        Debug.Log(comboCounter);

    }

    public override void Update()
    {
        base.Update();

        player.SetZeroVelocity();

        if (triggerCalled || Input.GetKeyDown(KeyCode.X) || stateTimer < 0)
        {
            stateMachine.ChangeState(player.idleState);
        }

        if (comboCounter == 1 )
        {
            stateMachine.ChangeState(player.enchant);
        }

    }
}
