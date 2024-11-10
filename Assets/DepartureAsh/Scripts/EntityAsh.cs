using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAsh : MonoBehaviour
{
    #region Components
    public Animator Anim { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    public EntityFXAsh Fx { get; private set; }
    #endregion
    [Header("Knockback info")]
    [SerializeField] protected Vector2 knockbackDirection_;
    [SerializeField] protected float knockbackDuration_;
    protected bool isKnocked_;

    [Header("Collision info")]
    public Transform attackCheck_;
    public float attackCheckRadius_;

    [SerializeField] protected Transform groundCheck_;
    [SerializeField] protected float groundCheckDistance_;
    [SerializeField] protected Transform wallCheck_;
    [SerializeField] protected float wallCheckDistance_;
    [SerializeField] protected LayerMask whatIsGround_;

    public int FacingDir { get; private set; } = 1;
    protected bool facingRight_ = true;

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        Fx = GetComponentInChildren<EntityFXAsh>();
        Anim = GetComponentInChildren<Animator>();
        Rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {

    }

    public virtual void Damage()
    {
       
        Fx.StartCoroutine("FlashFX");
        StartCoroutine("HitKnockback");
    }

    protected virtual IEnumerator HitKnockback()
    {
        isKnocked_ = true;

        Rb.velocity = new Vector2(knockbackDirection_.x * -FacingDir, knockbackDirection_.y);
        yield return new WaitForSeconds(knockbackDuration_);
        isKnocked_ = false;
    }
    #region Velocity
    public void SetZeroVelocity()
    {
        if (isKnocked_)
            return;
        Rb.velocity = new Vector2(0.0f, 0.0f);
    }
    public void SetVelocity(float xVelocity, float yVelocity)
    {
        if (isKnocked_)
            return;
        Rb.velocity = new Vector2(xVelocity, yVelocity);
        FlipController(xVelocity);
    }
    #endregion
    #region Collision
    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck_.position, Vector2.down, groundCheckDistance_, whatIsGround_);
    public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck_.position, Vector2.right * FacingDir, wallCheckDistance_, whatIsGround_);
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck_.position, new Vector3(groundCheck_.position.x, groundCheck_.position.y - groundCheckDistance_));
        Gizmos.DrawLine(wallCheck_.position, new Vector3(wallCheck_.position.x + wallCheckDistance_ * FacingDir, wallCheck_.position.y));
        Gizmos.DrawWireSphere(attackCheck_.position, attackCheckRadius_);

    }
    #endregion

    #region Flip
    public void Flip()
    {
        FacingDir = FacingDir * -1;
        facingRight_ = !facingRight_;
        transform.Rotate(0, 180, 0);
    }

    public void FlipController(float x)
    {
        if (x > 0 && !facingRight_)
            Flip();
        else if (x < 0 && facingRight_)
            Flip();
    }
    #endregion
}
