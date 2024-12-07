using UnityEngine;

public class CloneSkillControllerAX : MonoBehaviour
{

    private SpriteRenderer sr;
    private Animator anim;
    [SerializeField] private float colorLosingSpeed;
    private float cloneTimer;

    [SerializeField] private Transform attackCheck;
    [SerializeField] private float attackCheckHeight;
    [SerializeField] private float attackCheckWidth;
    private Transform closestEnemy;
    private bool canDuplicateClone;
    private int facingDir = 1;

    private float chanceToDuplicate;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        cloneTimer -= Time.deltaTime;

        if (cloneTimer < 0)
        {
            sr.color = new Color(1, 1, 1, sr.color.a - (Time.deltaTime * colorLosingSpeed));

            if (sr.color.a <= 0)
                Destroy(gameObject);
        }
    }

    //create clone
    public void SetupClone(Transform _newTransform, float _cloneDuration, bool _canAttack, Vector3 _offset, Transform _closestEnemy, bool _canDuplicate, float _chanceToDuplicate)
    {
        if (_canAttack)
            anim.SetInteger("AttackNumber", Random.Range(1, 3));


        transform.position = _newTransform.position + _offset;
        cloneTimer = _cloneDuration;
        closestEnemy = _closestEnemy;
        canDuplicateClone = _canDuplicate;
        chanceToDuplicate = _chanceToDuplicate;

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
            {
                hit.GetComponent<EnemyAX>().Damage();

                if (canDuplicateClone)
                {
                    if (Random.Range(0, 100) < chanceToDuplicate)
                    {
                        SkillManagerAX.instance.clone.CreateClone(hit.transform, new Vector3(5f * facingDir, 0));
                    }
                }
            }
        }
    }

    //find the closest enemy and fix the skill's function cannot set back the tranform variable
    private void FaceClosestTarger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 25);

        float closestDistance = Mathf.Infinity;

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<EnemyAX>() != null)
            {
                float distanceToEnemy = Vector2.Distance(transform.position, hit.transform.position);

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
            {
                facingDir = -1;
                transform.Rotate(0, 180, 0);
            }
        }
    }
}
