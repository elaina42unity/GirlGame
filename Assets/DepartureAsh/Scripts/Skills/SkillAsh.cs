using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAsh : MonoBehaviour
{
    [SerializeField] protected float cooldown_;

    protected float cooldownTimer_;

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
