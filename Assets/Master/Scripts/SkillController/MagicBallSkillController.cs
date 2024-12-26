using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class MagicBallSkillController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private CircleCollider2D cd;
    private Player player;
    private SpriteRenderer sr;
    private bool canAttack = true;
    private int damage;

    private float freezeTimeDuration;
    private float returnSpeed;

    [Header("WaterBall info")]
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

    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected LayerMask whatIsHalfGround;

    [Header("FireBall info")]
    private int fireBallAmount = 0;

    private void DestroyMe()
    {
        Destroy(gameObject);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        cd = GetComponent<CircleCollider2D>();
        sr = GetComponent<SpriteRenderer>();

    }
    private void Start()
    {
        
    }

    //get the basic information in skill script
    public void SetupMagic(Vector2 _dir, float _gravityScale, Player _player, float _freezeTimeDuration,int _damage)
    {
        player = _player;
        rb.velocity = _dir;
        rb.gravityScale = _gravityScale;
        freezeTimeDuration = _freezeTimeDuration;
        damage = _damage;
        anim.SetBool("Attack", true);

        //if player is facing the left flip the sprite
        if (rb.velocity.x < 0)
        {
            sr.flipY = true;
        }

        //
        if (fireBallAmount <= 0)
            anim.SetBool("Attack", true);

        spinDirection = Mathf.Clamp(rb.velocity.x, -1, 1);

        //when the attack prefab is too long or too far destroy it
        Invoke("DestroyMe", 3);
    }

    //get the information of bounce in skill script
    public void SetupWaterBall(bool _isBouncing, int _amountOfBounce, float _bounceSpeed)
    {
        isBouncing = _isBouncing;
        amountOfBounce = _amountOfBounce;
        bounceSpeed = _bounceSpeed;

        enemyTarget = new List<Transform>();
    }

    //get the information of fireball in skill script
    public void SetupFireBall(int _fireBallAmount)
    {
        fireBallAmount = _fireBallAmount;
    }

    //get the information of spin in skill script
    public void SetupFlashBall(bool _isSpinning, float _maxTravelDistance, float _spinDuration, float _hitCooldown)
    {
        isSpinning = _isSpinning;
        maxTravelDistance = _maxTravelDistance;
        spinDuration = _spinDuration;
        hitCooldown = _hitCooldown;
    }

    private void Update()
    {
        if (canAttack)
            transform.right = rb.velocity;

        BounceLogic();

        SpinLogic();
    }

    //flashball attack 
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
                canAttack = false;

                spinTimer -= Time.deltaTime;

                hitTimer -= Time.deltaTime;

                if (hitTimer < 0)
                {
                    hitTimer = hitCooldown;

                    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1);

                    foreach (var hit in colliders)
                    {
                        if (hit.GetComponent<Enemy>() != null)
                            MagicDamage(hit.GetComponent<Enemy>());

                    }
                }
                
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + spinDirection, transform.position.y), 1f * Time.deltaTime);
            }
        }
    }

    //initiate the spin timer
    private void StopWhenSpinning()
    {
        wasStopped = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        spinTimer = spinDuration;
    }

    //waterball's bouncing logic
    private void BounceLogic()
    {
        if (isBouncing && enemyTarget.Count > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, enemyTarget[targetIndex].position, bounceSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, enemyTarget[targetIndex].position) < .1f)
            {

                targetIndex++;
                amountOfBounce--;

                if (targetIndex >= enemyTarget.Count)
                    targetIndex = 0;
            }
        }
    }

    //when the collider is overlaped with other collider 
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.GetComponent<Enemy>() != null)
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            MagicDamage(enemy);

        }

        SetupTargetForBounce(collision);

        StuckInto(collision);
    }

    //take damage
    private void MagicDamage(Enemy enemy)
    {
        enemy.DamageEffect();
        enemy.stats.TakeDamage(damage);
        enemy.StartCoroutine("FreezeTimeFor", freezeTimeDuration);
    }

    //find the enemy to bounce to
    private void SetupTargetForBounce(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            if (isBouncing && enemyTarget.Count <= 0)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 40);

                foreach (var hit in colliders)
                {
                    if (hit.GetComponent<Enemy>() != null)
                        enemyTarget.Add(hit.GetComponent<Enemy>().transform);
                }
            }
        }
    }

    //the magic ball will stuck in the enemy's collider
    private void StuckInto(Collider2D collision)
    {
        if (fireBallAmount == 0)
            anim.SetBool("Attack", false);

        if (IsGroundDetected() || player.IsHalfGroundDetected())
        {
            fireBallAmount = 0;
            canAttack = false;
        }

        if (fireBallAmount > 0 && collision.GetComponent<Enemy>() != null)
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

    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    public virtual bool IsHalfGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsHalfGround);
}
    