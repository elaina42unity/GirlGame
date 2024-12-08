using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatsAX : CharacterStatsAX
{
    private EnemyAX enemy;
    protected override void Start()
    {
        base.Start();

        enemy = GetComponent<EnemyAX>();
    }

    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);

        enemy.DamageEffect();
    }

    protected override void Die()
    {
        base.Die();
        enemy.Die();
    }
}
