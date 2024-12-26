using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    public DashSkill dash { get; private set; }

    public CloneSkill clone { get; private set; }

    public PrimaryAttackMagic PAMagic { get; private set; }

    public BlackholeSkill blackhole { get; private set; }

    public StaffMagicSkill staffMagic { get; private set; }

    public StarMagicSkill starMagic { get; private set; }
    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        dash = GetComponent<DashSkill>();
        clone = GetComponent<CloneSkill>();
        PAMagic = GetComponent<PrimaryAttackMagic>();
        blackhole = GetComponent<BlackholeSkill>();
        staffMagic = GetComponent<StaffMagicSkill>();
        starMagic = GetComponent<StarMagicSkill>();
    }
}
