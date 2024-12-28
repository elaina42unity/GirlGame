using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    private int comboCounter;
    private bool skillused = true;

    private float lastTimeAttacked;
    private float comboWindow = 2;

    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

        //set a delay to the player make her unmovable
        player.StartCoroutine("BusyFor", .2f);

        comboCounter++;
        lastTimeAttacked = Time.time;

        //when exit the state create a magic ball
        if (skillused)
        {
            player.skill.PAMagic.CreateMagic(player.facingDir);
        }
    }

    //when she attack she cannnot move
    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            player.SetZeroVelocity();
        
        //when the trigger is false set state back to idle
        if (triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}