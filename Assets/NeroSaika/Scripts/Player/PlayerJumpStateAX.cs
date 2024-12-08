using UnityEngine;

public class PlayerJumpStateAX : PlayerStateAX
{
    public PlayerJumpStateAX(PlayerAX _player, PlayerStateMachineAX _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    //initiate
    public override void Enter()
    {
        base.Enter();

        rb.velocity = new Vector2(rb.velocity.x, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        //update state
        if (rb.velocity.y < 0)
            stateMachine.ChangeState(player.airState);
    }
}
