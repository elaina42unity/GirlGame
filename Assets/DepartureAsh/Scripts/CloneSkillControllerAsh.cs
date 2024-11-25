using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class CloneSkillControllerAsh : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] private Animator anim_;
    [SerializeField] private float colorLoosingSpeed_;

    private float cloneTimer_;
    [SerializeField] private Transform attackCheck_;
    [SerializeField] private float attackCheckRadius_ = .8f;
    private Transform closestEnemy;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim_ = GetComponent<Animator>();
    }

    private void Update()
    {
        cloneTimer_ -= Time.deltaTime;

        if (cloneTimer_ < 0)
        {
            sr.color = new Color(1, 1, 1, sr.color.a - (Time.deltaTime * colorLoosingSpeed_));

            if (sr.color.a <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetupClone(Transform newTransform, float cloneDuration, bool canAttack)
    {
        if (canAttack)
            anim_.SetInteger("AttackNumber", Random.Range(1, 3));

        transform.position = newTransform.position;
        cloneTimer_ = cloneDuration;

        FaceClosestTarget();
    }

    private void AnimationTrigger()
    {
        cloneTimer_ = -.1f;
    }
    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck_.position, attackCheckRadius_);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<EnemyAsh>() != null)
            {
                hit.GetComponent<EnemyAsh>().Damage();
            }
        }
    }

    private void FaceClosestTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position,25);

        float closestDistance = Mathf.Infinity;

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<EnemyAsh>() != null)
            {
                float distanceToEnemy = Vector2.Distance(transform.position,hit.transform.position);

                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = hit.transform;
                }
            }
        }

        if (closestEnemy != null)
        {
            if (transform.position.x > closestEnemy.position.x)
                transform.Rotate(0, 180, 0);
        }
    }
}
