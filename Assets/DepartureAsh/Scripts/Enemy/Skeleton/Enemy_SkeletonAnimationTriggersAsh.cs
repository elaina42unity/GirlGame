using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_SkeletonAnimationTriggersAsh : MonoBehaviour
{
    private Enemy_SkeletonAsh enemy_=>GetComponentInParent<Enemy_SkeletonAsh>();

    private void AnimationTrigger()
    {
        enemy_.AnimationFinishTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy_.attackCheck_.position, enemy_.attackCheckRadius_);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<PlayerAsh>() != null)
                hit.GetComponent<PlayerAsh>().Damage();
        }
    }

    private void OpenCounterWindow() => enemy_.OpenCounterAttackWindow();
    private void CloseCounterWindow() => enemy_.CloseCounterAttackWindow();
}
