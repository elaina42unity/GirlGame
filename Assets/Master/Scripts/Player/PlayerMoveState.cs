public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

        //when detected the wall
        if (player.IsWallDetected())
            player.SetZeroVelocity();

        //set the velocity of moving
        player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);

        //stop when there is a wall
        if (xInput == 0 || player.IsWallDetected())
            stateMachine.ChangeState(player.idleState);

    }
}

