using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackStateAsh : PlayerStateAsh
{

    private int comboCounter_;

    private float lastTimeAttacked_;
    private float comboWindow_ = 0.5f;


    public PlayerPrimaryAttackStateAsh(PlayerAsh player, PlayerStateMachineAsh stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (comboCounter_ > 2 || Time.time >= lastTimeAttacked_ + comboWindow_)
            comboCounter_ = 0;

        player_.Anim.SetInteger("ComboCounter", comboCounter_);
        //player_.Anim.speed = 1.1f;

        #region Choose attack direction
        float attackDir = player_.FacingDir;
        if (xInput_ != 0)
            attackDir = xInput_;

        #endregion
        player_.SetVelocity(player_.attackMovement_[comboCounter_].x * attackDir, player_.attackMovement_[comboCounter_].y);

        stateTimer = 0.1f; // 惯性

    }

    public override void Exit()
    {
        base.Exit();

        player_.StartCoroutine("BusyFor",0.15f);
        //player_.Anim.speed = 1;

        comboCounter_++;
        lastTimeAttacked_ = Time.time;

    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            player_.SetZeroVelocity();

        if (triggerCalled)
            stateMachine_.ChangeState(player_.IdleState);
    }
}
