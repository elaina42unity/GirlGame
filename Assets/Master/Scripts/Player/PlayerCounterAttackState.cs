using UnityEngine;

public class PlayerCounterAttackState : PlayerState
{
    private bool canCreateClone;

    public PlayerCounterAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    //initiate skill
    public override void Enter()
    {
        base.Enter();

        canCreateClone = true;

        stateTimer = player.counterAttackDuration;

        player.anim.SetBool("SuccessfulCounterAttack", false);

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        player.SetZeroVelocity();
        //set the attack range
        Collider2D[] colliders = Physics2D.OverlapBoxAll(player.counterAttackCheck.position, new Vector2(player.counterAttackCheckHeight, player.counterAttackCheckWidth), 0);

        //get if the counter attack box has appeared
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
                if (hit.GetComponent<Enemy>().CanBeStunned())
                {
                    stateTimer = 10;//any value bigger than 1
                    player.anim.SetBool("SuccessfulCounterAttack", true);
                    if (canCreateClone)
                    {
                        canCreateClone = false;
                        player.skill.clone.CanCreateCloneOnCounterAttack(hit.transform);
                    }
                }
        }

        //finish the skill
        if (stateTimer < .5f || triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
