using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSkileControllerAX : MonoBehaviour
{

    private SpriteRenderer sr;
    private Animator anim;
    [SerializeField] private float colorLosingSpeed;
    private float cloneTimer;

    [SerializeField] private Transform attackCheck;
    [SerializeField] private float attackCheckHeight;
    [SerializeField] private float attackCheckWidth;
    private Transform closestEnemy;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        cloneTimer -= Time.deltaTime;

        if(cloneTimer < 0)
        {
            sr.color = new Color(1, 1, 1, sr.color.a - (Time.deltaTime * colorLosingSpeed));

            if(sr.color.a <= 0 )
                Destroy(gameObject);
        }
    }
    public void SetupClone(Transform _newTransform,float _cloneDuration,bool _canAttack)
    {
        if (_canAttack)
            anim.SetInteger("AttackNumber", Random.Range(1, 3));

        transform.position = _newTransform.position;
        cloneTimer = _cloneDuration;

        FaceClosestTarger();
    }

    private void AnimationTrigger()
    {
        cloneTimer = -.1f;
    }

    private void AttackTrigger()
    {


        Collider2D[] colliders = Physics2D.OverlapBoxAll(attackCheck.position, new Vector2(attackCheckHeight, attackCheckWidth), 0);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<EnemyAX>() != null)
                hit.GetComponent<EnemyAX>().Damage();
        }
    }

    private void FaceClosestTarger()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(25, 25), 0);

        float closestDistance = Mathf.Infinity;

        foreach (var hit in colliders)
        {
            if(hit.GetComponent<EnemyAX>() != null)
            {
                float distanceToEnemy=Vector2.Distance(transform.position, hit.transform.position);

                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = hit.transform;
                }
            }
        }

        if(closestEnemy!= null)
        {
            if (transform.position.x > closestEnemy.position.x)
                transform.Rotate(0, 180, 0);
        }
    }
}
