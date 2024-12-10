using UnityEngine;

public class PlayerPrimaryAttackStateAX : PlayerStateAX
{
    private int comboCounter;
    private bool skillused;

    private float lastTimeAttacked;
    private float comboWindow = 2;

    public PlayerPrimaryAttackStateAX(PlayerAX _player, PlayerStateMachineAX _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        xInput = 0;     //reset the xinput to fix the attack direction

        //set the different attack animation on the combo number
        if (comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)
            comboCounter = 0;

        player.anim.SetInteger("ComboCounter", comboCounter);

        float attackDir = player.facingDir;

        //set the direction of attack
        if (xInput != 0)
            attackDir = xInput;

        //when attacking move a little bit
        player.SetVelocity(player.attackMovement[comboCounter].x * attackDir, player.attackMovement[comboCounter].y);

        stateTimer = .1f;       //reset the timer check the time between combo
    }

    public override void Exit()
    {
        base.Exit();

        player.StartCoroutine("BusyFor", .2f);

        comboCounter++;
        lastTimeAttacked = Time.time;

        skillused = true;
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            player.SetZeroVelocity();
        if (skillused)
        {
        player.skill.aimAttack.CreateMagic(player.facingDir);
            skillused = false;
        }

        if (triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
