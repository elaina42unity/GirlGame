using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRobot1AnimationTriggers : MonoBehaviour
{
    private EnemyRobot1 enemy => GetComponentInParent<EnemyRobot1>();

    private void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(enemy.attackCheck.position, new Vector2(enemy.attackCheckHeight, enemy.attackCheckWidth), 0);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Player>() != null)
            {
                PlayerStats target = hit.GetComponent<PlayerStats>();
                enemy.stats.DoDamage(target);
            }
        }
    }

    private void OpenCounterWindow() => enemy.OpenCounterAttackWindow();
    private void CloseCounterWindow() => enemy.CloseCounterAttackWindow();

}
