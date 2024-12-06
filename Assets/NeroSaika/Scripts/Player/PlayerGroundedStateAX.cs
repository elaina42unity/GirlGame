using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedStateAX : PlayerStateAX
{
    public PlayerGroundedStateAX(PlayerAX _player, PlayerStateMachineAX _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

        if (Input.GetKeyDown(KeyCode.Mouse1) && HasNoWaterBall())
            stateMachine.ChangeState(player.aimState);

        player.chantUsageTimer -= Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.Q))
            stateMachine.ChangeState(player.counterAttack);

        if (Input.GetKeyDown(KeyCode.Mouse0))
            stateMachine.ChangeState(player.primaryAttack);

        if (!player.IsGroundDetected())
            stateMachine.ChangeState(player.airState);

        if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected())
            stateMachine.ChangeState(player.jumpState);

        if (Input.GetKeyDown(KeyCode.Z) && player.IsGroundDetected())
            player.CheckforChantInput();

    }

    private bool HasNoWaterBall()
    {
        if (!player.waterBall)
        {
            return true;
        }

        player.waterBall.GetComponent<MagicBallSkillController>().ReturnWaterBall();
        return false;
    }
}
