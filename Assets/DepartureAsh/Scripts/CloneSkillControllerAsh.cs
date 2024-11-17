using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSkillControllerAsh : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] private Animator anim_;
    [SerializeField] private float colorLoosingSpeed_;

    private float cloneTimer_;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim_ = GetComponent<Animator>();
    }

    private void Update()
    {
        cloneTimer_ -= Time.deltaTime;

        if (cloneTimer_ < 0 )
        {
            sr.color=new Color(1,1,1,sr.color.a-(Time.deltaTime* colorLoosingSpeed_));
        }
    }

    public void SetupClone(Transform newTransform, float cloneDuration,bool canAttack)
    {
        if (canAttack)
            anim_.SetInteger("AttackNumber", Random.Range(1, 3));

        transform.position = newTransform.position;
        cloneTimer_ = cloneDuration;
    }

}
