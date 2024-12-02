using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAX : MonoBehaviour
{
    [Header("Move info")]
    public float moveSpeed = 12f;
    public float jumpForce;

    [Header("Dash info")]
    [SerializeField] private float dashCooldown;
    private float dashUsageTimer;
    public float dashSpeed;
    public float dashDuration;
    public float dashDir {  get; private set; }

    [Header("Collision info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;

    [Header("Dash info")]
    [SerializeField] private float chantCooldown;
    private float chantUsageTimer;
    //public float dashSpeed;
    public float chantDuration;
    public float chantDir { get; private set; }


    public int facingDir { get; private set; } = 1;
    private bool facingRight = false;

    #region Components
    public Animator anim {  get; private set; }

    public Rigidbody2D rb { get; private set; }

    #endregion


    #region States
    public PlayerStateMachineAX stateMachine {  get; private set; }

    public PlayerIdleStateAX idleState { get; private set; }

    public PlayerMoveStateAX moveState { get; private set; }

    public PlayerJumpStateAX jumpState { get; private set; }

    public PlayerAirStateAX airState { get; private set; }

    public PlayerWallSlideStateAX wallSlide { get; private set; }

    public PlayerDashStateAX dashState { get; private set; }

    public PlayerChantStateAX chantState { get; private set; }
    #endregion


    private void Awake()
    {
        stateMachine = new PlayerStateMachineAX();

        idleState = new PlayerIdleStateAX(this, stateMachine, "Idle");
        moveState = new PlayerMoveStateAX(this, stateMachine, "Move");
        jumpState = new PlayerJumpStateAX(this, stateMachine, "Jump");
        airState = new PlayerAirStateAX(this, stateMachine, "Jump");
        dashState = new PlayerDashStateAX(this, stateMachine, "Dash");
        wallSlide = new PlayerWallSlideStateAX(this, stateMachine, "WallSlide");
        chantState = new PlayerChantStateAX(this, stateMachine, "Chant");
    }

    private void Start()
    {
        anim=GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        stateMachine.Initialize(idleState);

    }

    private void Update()
    {
        stateMachine.currentState.Update();

        CheckforDashInput();

        

        Debug.Log(IsWallDetected());

    }

    private void CheckforDashInput()
    {
        dashUsageTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashUsageTimer < 0 )
        {
            dashUsageTimer = dashCooldown;
            dashDir = Input.GetAxisRaw("Horizontal");

            if (dashDir == 0)
                dashDir = facingDir;

            stateMachine.ChangeState(dashState);
        }
    }

    public void CheckforChantInput()
    {
        chantUsageTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Z) && chantUsageTimer < 0 && groundCheck)
        {
            chantUsageTimer = chantCooldown;
            chantDir = Input.GetAxisRaw("Horizontal");

            if (chantDir == 0)
                chantDir = facingDir;

            stateMachine.ChangeState(chantState);
        }
    }
    public void SetVelocity(float _xVelocity,float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(rb.velocity.x);
    }
    
    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    public bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }
    public void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    public void FlipController(float _x)
    {
        if (_x < 0 && !facingRight)
            Flip();
        else if (_x > 0 && facingRight)
            Flip();
    }
}
