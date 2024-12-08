using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggersAX : MonoBehaviour
{
    private PlayerAX player => GetComponentInParent<PlayerAX>();

    //when animation is over set the triggercall to be false
    private void AnimationTrigger()
    {
        player.AnimationFinishTrigger();
    }
    
    //Player's attack range 
    private void AttackTrigger()
    {

        Collider2D[] colliders = Physics2D.OverlapBoxAll(player.attackCheck.position, new Vector2(player.attackCheckHeight, player.attackCheckWidth), 0);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<EnemyAX>() != null)
            {
                EnemyStatsAX _target = hit.GetComponent<EnemyStatsAX>();
                player.stats.DoDamage(_target);        
            }
        }
    }

    //set the animation event when the AimAttack is over
    private void AimAttackSuccess()
    {
        SkillManagerAX.instance.aimAttack.CreateMagic();
    }
}
