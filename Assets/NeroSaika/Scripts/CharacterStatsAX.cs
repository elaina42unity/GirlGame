using UnityEngine;

public class CharacterStatsAX : MonoBehaviour
{
    [Header("Major stats")]
    public StatAX strength;     //1 point increase damage by 1 and crit.power by 1%
    public StatAX agility;      //1 point increase evasion by 1% and crit.chance by 1%
    public StatAX inteligence;  //1 point increase magic damage by 1 and magic resisitance by 3
    public StatAX vitality;     //1 point increase health by 3 or 5 points

    [Header("Offensive stats")]
    public StatAX damage;
    public StatAX critChance;
    public StatAX critPower;    //default value 150%

    [Header("defensive stats")]
    public StatAX maxHealth;
    public StatAX armor;
    public StatAX evasion;
    public StatAX magicResistance;

    [Header("Magic stats")]
    public StatAX fireDamage;
    public StatAX iceDamage;
    public StatAX linghtningDamage;
    public int igniteDamge;

    public bool isIgnited;      //does damage over time
    public bool isChilled;      //reduce armor by 20%
    public bool isShocked;      //reduce attack accuracy by 20%

    private float ignitedTimer;
    private float chilledTimer;
    private float shockedTimer;

    private float igniteDamageCooldown = .3f;
    private float igniteDamageTimer;

    //protected float ailmentTimer;


    [SerializeField] private int currentHealth;

    [SerializeField] private bool ismagicdamage;

    protected virtual void Start()
    {
        //set the critical damage to damage's 150%
        critPower.SetDefaultValue(150);

        //initiate health
        currentHealth = maxHealth.GetValue();
    }

    protected virtual void Update()
    {

        ignitedTimer -= Time.deltaTime;
        chilledTimer -= Time.deltaTime;
        shockedTimer -= Time.deltaTime;

        igniteDamageTimer -= Time.deltaTime;

        //if (ailmentTimer < 0)
        //{
        //    isIgnited = false;
        //    isChilled = false;
        //    isShocked = false;
        //}

        //during the time will be the ignited

        if (ignitedTimer < 0)
            isIgnited = false;

        if (chilledTimer < 0)
            isChilled = false;

        if (shockedTimer < 0)
            isShocked = false;

        // 1 time burn damage per igniteDamageCooldown
        if (igniteDamageTimer < 0 && isIgnited)
        {
            Debug.Log("Burn Damage" + igniteDamge);

            currentHealth -= igniteDamge;

            if (currentHealth < 0)
                Die();

            igniteDamageTimer = igniteDamageCooldown;
        }
    }

    public virtual void DoDamage(CharacterStatsAX _targetStats)
    {
        //when Aoid the attack
        if (TargetCanAvoidAttack(_targetStats))
        {
            Debug.Log("ATTACK AVOIDED");
            return;
        }

        //calculate the standard damage plus the character's strength
        int totalDamage = damage.GetValue() + strength.GetValue();

        //if ciritcal damage
        if (CanCrit())
        {
            Debug.Log("CRIT HIT");
            totalDamage = CalculateCriticalDamage(totalDamage);
        }

        //reduce the armor's data
        totalDamage = CheckTargetArmor(_targetStats, totalDamage);

        //target give itself's damage
        if (!ismagicdamage)
            _targetStats.TakeDamage(totalDamage);

        //make magical damage
        if (ismagicdamage)
            DoMagicDamage(_targetStats);

    }

    public virtual void DoMagicDamage(CharacterStatsAX _targetStats)
    {
        //get the damage data
        int _fireDamage = fireDamage.GetValue();
        int _iceDamage = iceDamage.GetValue();
        int _lightningDamage = linghtningDamage.GetValue();

        //calculate the magicDamage
        int totalMagicalDamage = _fireDamage + _iceDamage + _lightningDamage + inteligence.GetValue();

        //calculate the damage with magic resistance
        totalMagicalDamage = CheckTargetResistance(_targetStats, totalMagicalDamage);

        Debug.Log("MagicDamage:");

        if (Mathf.Max(_fireDamage, _iceDamage, _lightningDamage) <= 0)
            return;

        //target give itself's damage
        _targetStats.TakeDamage(totalMagicalDamage);

        bool canApplyIgnite = _fireDamage > _iceDamage && _fireDamage > _lightningDamage;
        bool canApplyChill = _iceDamage > _fireDamage && _iceDamage > _lightningDamage;
        bool canApplyShock = _lightningDamage > _iceDamage && _lightningDamage > _fireDamage;

        //when the damages are the same apply ailment by random
        while (!canApplyIgnite && !canApplyChill && !canApplyShock)
        {
            if (Random.value < .5f && _fireDamage > 0)
            {
                canApplyIgnite = true;
                _targetStats.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
                Debug.Log("applyed fire:");
                return;
            }

            if (Random.value < .5f && _iceDamage > 0)
            {
                canApplyChill = true;
                _targetStats.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
                Debug.Log("applyed ice:");
                return;
            }

            if (Random.value < .5f && _lightningDamage > 0)
            {
                canApplyShock = true;
                _targetStats.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
                Debug.Log("apply lightning:");
                return;
            }
        }

        //if is ignited get damage by 20% of firedamage
        if (canApplyIgnite)
            _targetStats.SetupIgniteDamage(Mathf.RoundToInt(_fireDamage * .2f));
        

        //check target's ailment
        _targetStats.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
    }

    private static int CheckTargetResistance(CharacterStatsAX _targetStats, int totalMagicalDamage)
    {
        //calculate the standard damage reduce the character's resistance and inteligence
        totalMagicalDamage -= _targetStats.magicResistance.GetValue() + (_targetStats.inteligence.GetValue() * 3);

        //make damage no less than 0
        totalMagicalDamage = Mathf.Clamp(totalMagicalDamage, 0, int.MaxValue);

        return totalMagicalDamage;
    }

    public void ApplyAilments(bool _ignite, bool _chill, bool _shock)
    {
        if (isIgnited || isChilled || isShocked)
            return;
        

        //when is ignited set the timer
        if (_ignite)
        {
            isIgnited = _ignite;
            ignitedTimer = 4;
            Debug.Log("is ignited");
        }

        //when is chilled set the timer
        if (_chill)
        {
            isChilled = _chill;
            chilledTimer = 4;
            Debug.Log("is chilled");
        }

        //when is shocked set the timer
        if (_shock)
        {
            isShocked = _shock;
            shockedTimer = 4;
            Debug.Log("is shock");
        }

    }

    public void SetupIgniteDamage(int _damage) => igniteDamge = _damage;
    public virtual void TakeDamage(int _damage)
    {
        //calculate the damage in the health
        currentHealth -= _damage;

        Debug.Log("Damage" + _damage);

        //if is dead
        if (currentHealth < 0)
            Die();
    }

    protected virtual void Die()
    {
        //throw new NotImplementedException();
    }

    private int CheckTargetArmor(CharacterStatsAX _targetStats, int totalDamage)
    {
        //if target is chilled the armor decrease by 20% when calculate the damage
        if (_targetStats.isChilled)
            totalDamage -= Mathf.RoundToInt(_targetStats.armor.GetValue() * .8f);
        else
            totalDamage -= _targetStats.armor.GetValue();

        //calculate the standard damage reduce the character's armor
        totalDamage -= _targetStats.armor.GetValue();

        //no less than 0
        totalDamage = Mathf.Clamp(totalDamage, 0, int.MaxValue);

        return totalDamage;
    }

    private bool TargetCanAvoidAttack(CharacterStatsAX _targetStats)
    {
        //can avoid attack's posibility whitch is calcultate by evansion plus agility
        int totalEvasion = _targetStats.evasion.GetValue() + _targetStats.agility.GetValue();

        //if character is shocked the posiblity that attack can be avoid increase by 20%
        if (isShocked)
            totalEvasion += 20;

        //if can avoid
        if (Random.Range(0, 100) < totalEvasion)
        {
            return true;
        }
        return false;
    }

    private bool CanCrit()
    {
        //calculate the critical damage posibility with chance plua agility
        int totalCriticalChance = critChance.GetValue() + agility.GetValue();

        //if can critical damage
        if (Random.Range(0, 100) <= totalCriticalChance)
        {
            return true;
        }
        return false;
    }

    private int CalculateCriticalDamage(int _damage)
    {
        //calculate the critical power with critpower and strength
        float totalCritPower = (critPower.GetValue() + strength.GetValue()) * .01f;

        //calculate the critical power with power
        float critDamage = _damage * totalCritPower;

        return Mathf.RoundToInt(critDamage);
    }
}
