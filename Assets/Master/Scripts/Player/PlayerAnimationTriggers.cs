using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();

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
            if (hit.GetComponent<Enemy>() != null)
            {
                EnemyStats _target = hit.GetComponent<EnemyStats>();
                
                player.stats.DoDamage(_target);        
            }
        }
    }

    //set the animation event when the AimAttack is over
    private void AimAttackSuccess()
    {
        //SkillManager.instance.aimAttack.CreateMagic();
    }
}
