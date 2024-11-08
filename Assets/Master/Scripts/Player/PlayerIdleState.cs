using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
       public PlayerIdleState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player_.ZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (xInput_ == player_.FacingDir && player_.IsWallDetected()) // do nothing if the direction the player wants to move towards is a wall
            return;
        else if (xInput_ != 0 ) // if the player wants to move, then change the state to move
            stateMachine_.ChangeState(player_.MoveState);
           
    }
}
