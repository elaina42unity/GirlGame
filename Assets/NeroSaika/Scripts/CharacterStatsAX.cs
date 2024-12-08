using UnityEngine;

public class CharacterStatsAX : MonoBehaviour
{
    [Header("Major stats")]
    public StatAX strength;     //1 point increase damage by 1 and crit.power by 1%
    public StatAX agility;      //1 point increase evasion by 1% and crit.chance by 1%
    public StatAX inteligence;  //1 point increase magic damage by 1 and magic resisitance by 3
    public StatAX vitality;     //1 point increase health by 3 or 5 points

    [Header("defensive stats")]
    public StatAX maxHealth;
    public StatAX armor;
    public StatAX evasion;

    public StatAX damage;

    [SerializeField] private int currentHealth;

    protected virtual void Start()
    {
        currentHealth = maxHealth.GetValue();
    }

    public virtual void DoDamage(CharacterStatsAX _targetStats)
    {
        if (TargetCanAvoidAttack(_targetStats))
            return;

        int totalDamage = damage.GetValue() + strength.GetValue();
        totalDamage = CheckTargetArmor(_targetStats, totalDamage);

        _targetStats.TakeDamage(totalDamage);
    }


    public virtual void TakeDamage(int _damage)
    {
        currentHealth -= _damage;



        if (currentHealth < 0)
            Die();
    }

    protected virtual void Die()
    {
        //throw new NotImplementedException();
    }

    private int CheckTargetArmor(CharacterStatsAX _targetStats, int totalDamage)
    {
        totalDamage -= _targetStats.armor.GetValue();
        totalDamage = Mathf.Clamp(totalDamage, 0, int.MaxValue);
        return totalDamage;
    }

    private bool TargetCanAvoidAttack(CharacterStatsAX _targetStats)
    {
        int totalEvasion = _targetStats.evasion.GetValue() + _targetStats.agility.GetValue();

        if (Random.Range(0, 100) < totalEvasion)
        {
            return true;
        }
        return false;
    }
}
