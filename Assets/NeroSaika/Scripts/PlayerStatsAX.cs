using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsAX : CharacterStatsAX
{
    private PlayerAX player;

    protected override void Start()
    {
        base.Start();

        player = GetComponent<PlayerAX>();
    }

    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);

        player.DamageEffect();
    }

    protected override void Die()
    {
        base.Die();

        player.Die();
    }
}
