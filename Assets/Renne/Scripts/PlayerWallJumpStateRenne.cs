using UnityEngine;

public class PlayerWallJumpStateRenne : PlayerStateRenne
{
    public PlayerWallJumpStateRenne(PlayerRenne _player, PlayerStateMachineRenne _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = 1.0f;
        player.SetVelocity(5 * -player.facingDir, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
        {
            stateMachine.ChangesState(player.airState);
        }

        if (player.IsGroundDetected())
        {
            stateMachine.ChangesState(player.idleState);
        }
    }
}
