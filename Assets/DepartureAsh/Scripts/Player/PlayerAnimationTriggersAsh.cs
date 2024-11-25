using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggersAsh : MonoBehaviour
{
    private PlayerAsh player_ => GetComponentInParent<PlayerAsh>();

    private void AnimationTrigger()
    {
        player_.AnimationTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player_.attackCheck_.position, player_.attackCheckRadius_);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<EnemyAsh>()!=null)
            {
                hit.GetComponent<EnemyAsh>().Damage();
            }
        }
    }

    private void ThrowSword()
    {
        SkillManagerAsh.instance_.Sword.CreateSword();
    }
}
