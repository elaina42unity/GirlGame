using UnityEngine;

public class SkillManagerAX : MonoBehaviour
{
    public static SkillManagerAX instance;

    public DashSkillAX dash { get; private set; }

    public CloneSkillAX clone { get; private set; }

    public AimAttackAX aimAttack { get; private set; }

    public BlackholeSkillAX blackhole { get; private set; }

    public StaffMagicSkillAX staffMagic { get; private set; }

    public StarMagicSkillAX starMagic { get; private set; }
    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        dash = GetComponent<DashSkillAX>();
        clone = GetComponent<CloneSkillAX>();
        aimAttack = GetComponent<AimAttackAX>();
        blackhole = GetComponent<BlackholeSkillAX>();
        staffMagic = GetComponent<StaffMagicSkillAX>();
        starMagic = GetComponent<StarMagicSkillAX>();
    }
}
