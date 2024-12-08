using System.Collections.Generic;
using UnityEngine;

public class MagicBallSkillController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private CircleCollider2D cd;
    private PlayerAX player;
    private bool canAttack = true;
    private bool isReturning;

    private float freezeTimeDuration;
    private float returnSpeed;

    [Header("WaterBall(bounce) info")]
    private float bounceSpeed;
    private bool isBouncing;
    private int amountOfBounce;
    public List<Transform> enemyTarget;
    private int targetIndex;

    [Header("Spin info")]
    private float maxTravelDistance;
    private float spinDuration;
    private float spinTimer;
    private bool wasStopped;
    private bool isSpinning;

    private float hitTimer;
    private float hitCooldown;

    private float spinDirection;

    [Header("FireBall(pierce) info")]
    private int fireBallAmount;

    private void DestroyMe()
    {
        Destroy(gameObject);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        cd = GetComponent<CircleCollider2D>();
    }
    private void Start()
    {
    }

    //create magic
    public void SetupMagic(Vector2 _dir, float _gravityScale, PlayerAX _player, float _freezeTimeDuration, float _returnSpeed)
    {
        player = _player;
        rb.velocity = _dir;
        rb.gravityScale = _gravityScale;
        freezeTimeDuration = _freezeTimeDuration;
        returnSpeed = _returnSpeed;
        anim.SetBool("Attack", true);

        if (fireBallAmount < 0)
            anim.SetBool("Attack", true);

        spinDirection = Mathf.Clamp(rb.velocity.x, -1, 1);

        //when the attack prefab is too long or too far destroy it
        Invoke("DestroyMe", 5);
    }

    public void SetupWaterBall(bool _isBouncing, int _amountOfBounce, float _bounceSpeed)
    {
        isBouncing = _isBouncing;
        amountOfBounce = _amountOfBounce;
        bounceSpeed = _bounceSpeed;

        enemyTarget = new List<Transform>();
    }

    public void SetupFireBall(int _fireBallAmount)
    {
        fireBallAmount = _fireBallAmount;
    }

    public void SetupFlashBall(bool _isSpinning, float _maxTravelDistance, float _spinDuration, float _hitCooldown)
    {
        isSpinning = _isSpinning;
        maxTravelDistance = _maxTravelDistance;
        spinDuration = _spinDuration;
        hitCooldown = _hitCooldown;
    }

    public void ReturnWaterBall()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        rb.isKinematic = false;
        transform.parent = null;
        isReturning = true;
    }

    private void Update()
    {
        if (canAttack)
            transform.right = rb.velocity;

        if (isReturning)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, returnSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, player.transform.position) < 2)
                player.CatchTheWaterBall();
        }

        BounceLogic();

        SpinLogic();
    }

    private void SpinLogic()
    {
        if (isSpinning)
        {
            if (Vector2.Distance(player.transform.position, transform.position) >= maxTravelDistance)
            {
                StopWhenSpinning();
            }

            if (wasStopped)
            {
                spinTimer -= Time.deltaTime;

                if (spinTimer < 0)
                {

                    isReturning = true;
                    isSpinning = false;
                    return;
                }

                hitTimer -= Time.deltaTime;

                if (hitTimer < 0)
                {
                    hitTimer = hitCooldown;

                    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1);

                    foreach (var hit in colliders)
                    {
                        if (hit.GetComponent<EnemyAX>() != null)
                            hit.GetComponent<EnemyAX>().DamageEffect();
                    }
                }

                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + spinDirection, transform.position.y), .3f * Time.deltaTime);
            }
        }
    }

    private void StopWhenSpinning()
    {
        wasStopped = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        spinTimer = spinDuration;
    }

    private void BounceLogic()
    {
        if (isBouncing && enemyTarget.Count > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, enemyTarget[targetIndex].position, bounceSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, enemyTarget[targetIndex].position) < .1f)
            {

                MagicDamage(enemyTarget[targetIndex].GetComponent<EnemyAX>());

                targetIndex++;
                amountOfBounce--;

                if (amountOfBounce <= 0)
                {
                    isBouncing = false;
                    isReturning = true;
                }

                if (targetIndex >= enemyTarget.Count)
                    targetIndex = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isReturning)
            return;

        if (collision.GetComponent<EnemyAX>() != null)
        {
            EnemyAX enemy = collision.GetComponent<EnemyAX>();
            MagicDamage(enemy);

        }

        SetupTargetForBounce(collision);

        StuckInto(collision);
    }

    private void MagicDamage(EnemyAX enemy)
    {
        enemy.DamageEffect();

        enemy.StartCoroutine("FreezeTimeFor", freezeTimeDuration);
    }

    //find the enemy to bounce to
    private void SetupTargetForBounce(Collider2D collision)
    {
        if (collision.GetComponent<EnemyAX>() != null)
        {
            if (isBouncing && enemyTarget.Count <= 0)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 40);

                foreach (var hit in colliders)
                {
                    if (hit.GetComponent<EnemyAX>() != null)
                        MagicDamage(hit.GetComponent<EnemyAX>());
                }
            }

        }
    }

    private void StuckInto(Collider2D collision)
    {

        anim.SetBool("Attack", false);
        if (fireBallAmount > 0 && collision.GetComponent<EnemyAX>() != null)
        {
            fireBallAmount--;
            return;
        }

        if (isSpinning)
        {
            StopWhenSpinning();
            return;
        }

        canAttack = false;
        cd.enabled = false;

        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        if (isBouncing && enemyTarget.Count > 0)
            return;

        transform.parent = collision.transform;
    }
}
