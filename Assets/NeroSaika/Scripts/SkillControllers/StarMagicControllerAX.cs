using UnityEngine;

public class StarMagicControllerAX : MonoBehaviour
{
    private Animator anim => GetComponent<Animator>();
    private CircleCollider2D cd => GetComponent<CircleCollider2D>();

    private float starExistTimer;

    private bool canExplode;
    private bool canMove;
    private float moveSpeed;
    private float defaultmoveSpeed;

    private bool canGrow;
    private float growSpeed = 5;

    private Transform closestTarget;
    [SerializeField] private LayerMask whatisEnemy;

    public void SetupStar(float _starDuration, bool _canExplode, bool _canMoveToEnemy, float _moveSpeed, Transform _closestTarget)
    {
        starExistTimer = _starDuration;
        canExplode = _canExplode;
        canMove = _canMoveToEnemy;
        moveSpeed = _moveSpeed;
        closestTarget = _closestTarget;
    }

    public void ChooseRandomEnemy()
    {
        float radius = SkillManagerAX.instance.blackhole.GetBlackholeRadius();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 25, whatisEnemy);

        if (colliders.Length > 0)
            closestTarget = colliders[Random.Range(0, colliders.Length)].transform;
    }

    private void Update()
    {
        starExistTimer -= Time.deltaTime;

        if (starExistTimer < 0)
        {
            FinishStar();
        }

        //when there is a target star will move to the target
        if (canMove && closestTarget != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, closestTarget.position, moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, closestTarget.position) < 2)
            {
                FinishStar();
                canMove = false;
            }
        }

        //explode check
        if (canGrow)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector3(16, 16), growSpeed * Time.deltaTime);
        }
    }

    private void AnimationExplodeEvent()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, cd.radius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<EnemyAX>() != null)
            {
                hit.GetComponent<EnemyAX>().Damage();
            }
        }
    }

    public void FinishStar()
    {
        if (canExplode)
        {
            canGrow = true;
            anim.SetTrigger("Explode");
        }
        else
            SelfDestroy();
    }

    public void SelfDestroy() => Destroy(gameObject);

    public virtual void FreezeTime(bool _timeFrozen)
    {
        defaultmoveSpeed = moveSpeed;

        if (_timeFrozen)
        {
            moveSpeed = 0;
            anim.speed = 0;
        }
        else
        {
            moveSpeed = defaultmoveSpeed;
            anim.speed = 1;
        }
    }
}
