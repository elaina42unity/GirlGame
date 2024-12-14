public class PlayerBombFlashState : PlayerState
{
    public PlayerBombFlashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.isdodging = true;

        player.skill.clone.CreateCloneOnDodgeStart();           //make a clone skill when use bombflash

        stateTimer = player.bombFlashDuration;                  //initiate the timer
    }

    public override void Exit()
    {
        base.Exit();

        player.isdodging = false;

        player.Flip();

        player.skill.clone.CreateCloneOnDodgeOver();            //make a clone skill when finish bombflash

        player.SetVelocity(0, rb.velocity.y);                   //reset the velocity
    }

    public override void Update()
    {
        base.Update();

        //the skill will move backwards to the facing direction
        player.SetVelocity(player.bombFlashSpeed * -player.bombFlashDir, 0);

        //update the state
        if ((stateTimer < 0))
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
