using UnityEngine;

public class PlayerWallSlideStateAX : PlayerStateAX
{
    public PlayerWallSlideStateAX(PlayerAX _player, PlayerStateMachineAX _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

        //iput check
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.wallJump);
            return;
        }

        //check if facing is opposite to the wall
        if (xInput != 0 && player.facingDir != xInput)
            stateMachine.ChangeState(player.idleState);

        //check input
        if (yInput < 0)
            rb.velocity = new Vector2(0, rb.velocity.y);
        else 
            rb.velocity = new Vector2(0, rb.velocity.y * .7f);

        rb.velocity = new Vector2(0, rb.velocity.y);

        //update to ground state
        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.idleState);


    }
}
