using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggersAX : MonoBehaviour
{
    private PlayerAX player => GetComponentInParent<PlayerAX>();

    private void AnimationTrigger()
    {
        player.AnimationFinishTrigger();
    }
    
    private void AttackTrigger()
    {


        Collider2D[] colliders = Physics2D.OverlapBoxAll(player.attackCheck.position, new Vector2(player.attackCheckHeight, player.attackCheckWidth), 0);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<EnemyAX>() != null)
                hit.GetComponent<EnemyAX>().Damage();
        }
    }

    private void AimAttackSuccess()
    {
        SkillManagerAX.instance.aimAttack.CreateWaterBall();
    }
}
