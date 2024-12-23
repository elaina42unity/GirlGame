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

    //when is grounded check the input
    public override void Update()
    {
        base.Update();

        if ((Input.GetKeyDown(KeyCode.R) && SkillManagerAX.instance.blackhole.CanUseSkill())
            ||(Input.GetKeyDown(KeyCode.Joystick1Button4) && SkillManagerAX.instance.blackhole.CanUseSkill()))
            stateMachine.ChangeState(player.blackHole);

        if (Input.GetKeyDown(KeyCode.Mouse1) && HasNoWaterBall())
            stateMachine.ChangeState(player.aimState);

        //player.chantUsageTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Q)|| Input.GetKeyDown(KeyCode.Joystick1Button3))
            stateMachine.ChangeState(player.counterAttack);

        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Joystick1Button0))
            stateMachine.ChangeState(player.primaryAttack);

        if (!player.IsGroundDetected())
            stateMachine.ChangeState(player.airState);

        if ((Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected())
            || (Input.GetKeyDown(KeyCode.Joystick1Button1) && player.IsGroundDetected()))
            stateMachine.ChangeState(player.jumpState);

        if (Input.GetKeyDown(KeyCode.Z) && player.IsGroundDetected())
            player.CheckforChantInput();

        if (Input.GetKeyDown(KeyCode.G) && player.IsGroundDetected()
            || Input.GetKeyDown(KeyCode.Joystick1Button2) && player.IsGroundDetected() && yInput < 0)
            player.skill.staffMagic.CanUseSkill();

    }

    //check the waterball state
    private bool HasNoWaterBall()
    {
        if (!player.waterBall)
        {
            return true;
        }

        player.waterBall.GetComponent<MagicBallSkillControllerAX>().ReturnWaterBall();
        return false;
    }


}
