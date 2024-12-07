using UnityEngine;

public class PlayerBlackholeStateAX : PlayerStateAX
{
    private bool skillUsed;         //skill can be used or not 

    private float defaultGravity;   //player's normal gravity

    public PlayerBlackholeStateAX(PlayerAX _player, PlayerStateMachineAX _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    //inherit animationg stop
    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    //initiate status of the skill
    public override void Enter()
    {
        base.Enter();

        defaultGravity = player.rb.gravityScale;

        skillUsed = false;

        rb.gravityScale = 0;
    }


    public override void Exit()
    {
        base.Exit();

        player.rb.gravityScale = defaultGravity;    //reset the gravity

        player.MakeTransparent(false);              //reset the transparence

    }

    public override void Update()
    {
        base.Update();

        if (stateTimer > 0)
            rb.velocity = new Vector2(0, 15);               //set the new position

        if (stateTimer < 0)
        {
            rb.velocity = new Vector2(0, -.1f);             //set the new position

            if (!skillUsed)
            {
                player.skill.blackhole.CanUseSkill();       //if the skill can be use then use it
                skillUsed = true;
            }
        }

        if (player.skill.blackhole.SkillCompleted())        //finish the skill
            stateMachine.ChangeState(player.airState);
    }
}
