using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAsh : MonoBehaviour
{
    [Header("Move info")]
    public float moveSpeed_ = 12f;
    public float jumpForce_;

    [Header("Dash info")]
    [SerializeField] private float dashCooldown_;
    private float dashUsageTime_;
    public float dashSpeed_;
    public float dashDuration_;
    public float DashDir { get; private set; }

    [Header("Collision info")]
    [SerializeField] private Transform groundCheck_;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Transform wallCheck_;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;

    public int facingDir { get; private set; } = 1;
    private bool facingRight = true;

    #region Components
    public Animator Anim { get; private set; }

    public Rigidbody2D Rb { get; private set; }
    #endregion

    #region States
    public PlayerStateMachineAsh StateMachine { get; private set; }
    public PlayerIdleStateAsh IdleState { get; private set; }
    public PlayerMoveStateAsh MoveState { get; private set; }
    public PlayerJumpStateAsh JumpState { get; private set; }
    public PlayerAirStateAsh AirState { get; private set; }
    public PlayerWallSlideStateAsh WallSlideState { get; private set; }
    public PlayerWallJumpStateAsh WallJumpState { get; private set; }
    public PlayerDashStateAsh DashState { get; private set; }

    public PlayerPrimaryAttackStateAsh PrimaryAttackState { get; private set; }
    #endregion


    private void Awake()
    {
        StateMachine = new PlayerStateMachineAsh();

        IdleState = new PlayerIdleStateAsh(this, StateMachine, "Idle");
        MoveState = new PlayerMoveStateAsh(this, StateMachine, "Move");
        JumpState = new PlayerJumpStateAsh(this, StateMachine, "Jump");
        AirState = new PlayerAirStateAsh(this, StateMachine, "Jump");
        DashState = new PlayerDashStateAsh(this, StateMachine, "Dash");
        WallSlideState = new PlayerWallSlideStateAsh(this, StateMachine, "WallSlide");
        WallJumpState = new PlayerWallJumpStateAsh(this, StateMachine, "Jump");

        PrimaryAttackState = new PlayerPrimaryAttackStateAsh(this, StateMachine, "Attack");
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        Rb = GetComponent<Rigidbody2D>();

        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        StateMachine.CurrentState.Update();

        CheckForDashInput();
    }

    public void AnimationTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    private void CheckForDashInput()
    {
        if (IsWallDetected())
            return;

        dashUsageTime_ -= Time.deltaTime;
        if (dashUsageTime_ < -10000.0f)
        {
            dashUsageTime_ = -0.1f;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashUsageTime_ < 0)
        {
            dashUsageTime_ = dashCooldown_;
            DashDir = Input.GetAxisRaw("Horizontal");

            if (DashDir == 0)
                DashDir = facingDir;

            // 如果没有输入方向就不进行冲刺的处理方法
            //if (DashDir!=0)
            //    StateMachine.ChangeState(DashState);

            StateMachine.ChangeState(DashState);
        }
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        Rb.velocity = new Vector2(xVelocity, yVelocity);
        FlipController(xVelocity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck_.position, new Vector3(groundCheck_.position.x, groundCheck_.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck_.position, new Vector3(wallCheck_.position.x + wallCheckDistance, wallCheck_.position.y));

    }

    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck_.position, Vector2.down, groundCheckDistance, whatIsGround);
    public bool IsWallDetected() => Physics2D.Raycast(wallCheck_.position, Vector2.right, wallCheckDistance, whatIsGround);

    public void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    public void FlipController(float x)
    {
        if (x > 0 && !facingRight)
            Flip();
        else if (x < 0 && facingRight)
            Flip();
    }
}
