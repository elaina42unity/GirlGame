using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMonster1AnimationTriggers : MonoBehaviour
{
    private EnemyMonster1 enemy => GetComponentInParent<EnemyMonster1>();

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

    private  void ColliderDamageTrigger()
    {
        
        Collider2D[] colliders = Physics2D.OverlapBoxAll(enemy.transform.position, new Vector2(enemy.cdDamageWidth, enemy.cdDamageHeight), 0);
        if (enemy.cdDamageCooldownTimer < 0)
        {
            foreach (var hit in colliders)
            {
                if (hit.GetComponent<Player>() != null)
                {
                    PlayerStats target = hit.GetComponent<PlayerStats>();
                    enemy.stats.DoDamage(target);
                }
            }
            enemy.cdDamageCooldownTimer = enemy.cdDamageCooldown;
        }
    }

    private void OpenCounterWindow() => enemy.OpenCounterAttackWindow();
    private void CloseCounterWindow() => enemy.CloseCounterAttackWindow();

}
