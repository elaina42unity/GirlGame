using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManagerAX : MonoBehaviour
{
    public static SkillManagerAX instance;

    public DashSkillAX dash {  get; private set; }

    public CloneSkillAX clone { get; private set; }

    public AimAttackAX aimAttack { get; private set; }
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
    }
}
