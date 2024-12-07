using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimStateAX : PlayerStateAX
{
    public PlayerAimStateAX(PlayerAX _player, PlayerStateMachineAX _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //Make Dots to be Active to set the position
        player.skill.aimAttack.DotsActive(true);
    }

    public override void Exit()
    {
        base.Exit();

        //Player will not be moving between the attack combo or aim attack
        player.StartCoroutine("BusyFor", .2f);
    }

    public override void Update()
    {
        base.Update();

        player.SetZeroVelocity();           //In the aimstate player cannot move

        //aim
        if (Input.GetKeyUp(KeyCode.Mouse1))
            stateMachine.ChangeState(player.idleState);

        //get the mousePosition to aim 
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //if mouse is on the left playerwill turn left,if it is on the right player will turn right
        if (player.transform.position.x > mousePosition.x && player.facingDir == 1)
            player.Flip();
        else if(player.transform.position.x < mousePosition.x&&player.facingDir == -1)
            player.Flip();
    }
}
