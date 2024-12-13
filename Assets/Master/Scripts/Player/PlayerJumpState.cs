using UnityEngine;

public class PlayerJumpState : PlayerState
{
    private float jumpSpeed;
    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    //initiate
    public override void Enter()
    {
        base.Enter();

        stateTimer = 1f;

        jumpSpeed = player.jumpForce;

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        stateTimer -= Time.deltaTime;

        jumpSpeed -= .05f;
        player.FlipController(player.facingDir);

        if (stateTimer >= 0f)
            player.SetVelocity(xInput * player.moveSpeed, 4 * jumpSpeed);

        //update state
        if (rb.velocity.y < 0)
            stateMachine.ChangeState(player.airState);
    }
}
