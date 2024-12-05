using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAX : MonoBehaviour
{
    [SerializeField] protected float cooldown;
    protected float cooldownTimer;

    protected PlayerAX player;

    protected virtual void Start()
    {
        player = PlayerManagerAX.instance.player;
    }

    protected virtual void Update()
    {
        cooldownTimer -= Time.deltaTime;
    }
    public virtual bool CanUseSkill()
    {
        if(cooldownTimer < 0)
        {
            //use skill
            cooldownTimer = cooldown;
            return true;
        }
        return false;
    }

    public virtual void UseSkill()
    {

    }
}
