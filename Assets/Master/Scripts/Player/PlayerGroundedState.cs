using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

    //when is grounded check the input
    public override void Update()
    {
        base.Update();

        //if (Input.GetKeyDown(KeyCode.R) && SkillManager.instance.blackhole.CanUseSkill())
        //    stateMachine.ChangeState(player.blackHole);

        //if (Input.GetKeyDown(KeyCode.Mouse1) && HasNoWaterBall())
        //    stateMachine.ChangeState(player.aimState);

        //player.chantUsageTimer -= Time.deltaTime;

        //if (Input.GetKeyDown(KeyCode.Q))
        //    stateMachine.ChangeState(player.counterAttack);

        if (Input.GetKeyDown(KeyCode.Mouse0))
            stateMachine.ChangeState(player.primaryAttack);

        if (!player.IsGroundDetected())
            stateMachine.ChangeState(player.airState);

        if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected())
            stateMachine.ChangeState(player.jumpState);

        //if (Input.GetKeyDown(KeyCode.Z) && player.IsGroundDetected())
        //    player.CheckforChantInput();

    }

    //check the waterball state
    //private bool HasNoWaterBall()
    //{
    //    if (!player.waterBall)
    //    {
    //        return true;
    //    }

    //    player.waterBall.GetComponent<MagicBallSkillControllerAX>().ReturnWaterBall();
    //    return false;
    //}
}
