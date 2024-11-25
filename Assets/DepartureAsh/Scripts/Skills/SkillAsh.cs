using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 分离技能类和技能对象，技能类只负责创建，之后的交互等等逻辑交给控制器类
/// </summary>
public class SkillAsh : MonoBehaviour
{
    [SerializeField] protected float cooldown_;

    protected float cooldownTimer_;

    protected PlayerAsh player_;

    protected virtual void Start()
    {
        player_ = PlayerManagerAsh.instance_.player_;
    }

    protected virtual void Update()
    {
        cooldownTimer_ -= Time.deltaTime;
        if (cooldownTimer_ < -10000.0f)
        {
            cooldownTimer_ = -0.1f;
        }
    }

    public virtual bool CanUseSkill()
    {
        if (cooldownTimer_<0)
        {
            UseSkill();
            cooldownTimer_= cooldown_;
            return true;
        }

        Debug.Log("Skill is on cooldown");
        return false;
    }

    public virtual void UseSkill()
    {
        // do some skill specific things
    }
}
