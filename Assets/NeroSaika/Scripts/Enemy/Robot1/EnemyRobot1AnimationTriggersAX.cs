using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRobot1AnimationTriggersAX : MonoBehaviour
{
    private EnemyRobot1AX enemy => GetComponentInParent<EnemyRobot1AX>();

    private void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(enemy.attackCheck.position, new Vector2(enemy.attackCheckHeight, enemy.attackCheckWidth), 0);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<PlayerAX>() != null)
                hit.GetComponent<PlayerAX>().Damage();
        }
    }

    private void OpenCounterWindow() => enemy.OpenCounterAttackWindow();
    private void CloseCounterWindow() => enemy.CloseCounterAttackWindow();

}
