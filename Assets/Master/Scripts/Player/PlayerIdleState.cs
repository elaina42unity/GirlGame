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

        PlayerObject.SetZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (XInput == PlayerObject.FacingDir && PlayerObject.IsWallDetected()) // do nothing if the direction the player wants to move towards is a wall
            return;
        else if (XInput != 0 ) // if the player wants to move, then change the state to move
            stateMachine_.ChangeState(PlayerObject.MoveState);
           
    }
}
