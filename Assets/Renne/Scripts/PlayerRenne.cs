using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerRenne : MonoBehaviour
{
    [Header("Attack details")]
    public Vector2[] attackMovement;
    public bool isBusy { get; private set; }
    [Header("Move info")]
    public float moveSpeed;
    public float jumpForce;

    [Header("Dash info")]
    [SerializeField] private float dashCooldown;
    private float dashUsageTimer;
    public float dashSpeed;
    public float dashDuration;
    public float dashDir { get; private set; }


    [Header("Collision info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsHalfGround;

    public int facingDir { get; private set; } = 1;
    private bool facingRight = true;

    #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public Collider2D rbHalfGround { get; private set; }
    #endregion
    #region States

    public PlayerStateMachineRenne stateMachine { get; private set; }

    public PlayerIdleStateRenne idleState { get; private set; }
    public PlayerMoveStateRenne moveState { get; private set; }
    public PlayerJumpStateRenne jumpState { get; private set; }
    public PlayerAirStateRenne airState { get; private set; }
    public PlayerWallSlideStateRenne wallSlideState { get; private set; }
    public PlayerWallJumpStateRenne wallJumpState { get; private set; }
    public PlayerDashStateRenne dashState { get; private set; }

    public PlayerPrimaryAttackStateRenne primaryAttackState { get; private set; }
    #endregion

    private void Awake()
    {
        stateMachine = new PlayerStateMachineRenne();

        idleState = new PlayerIdleStateRenne(this, stateMachine, "Idle");
        moveState = new PlayerMoveStateRenne(this, stateMachine, "Move");
        jumpState = new PlayerJumpStateRenne(this, stateMachine, "Jump");
        airState = new PlayerAirStateRenne(this, stateMachine, "Jump");
        dashState = new PlayerDashStateRenne(this, stateMachine, "Dash");
        wallSlideState = new PlayerWallSlideStateRenne(this, stateMachine, "WallSlide");
        wallJumpState = new PlayerWallJumpStateRenne(this, stateMachine, "Jump");

        primaryAttackState = new PlayerPrimaryAttackStateRenne(this, stateMachine, "Attack");
    }

    public void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        stateMachine.currentState.Update();
        CheckForDashInput();
    }

    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;

        yield return new WaitForSeconds(_seconds);

        isBusy = false;
    }

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    private void CheckForDashInput()
    {
        if (IsWallDetected())
        {
            return;
        }

        dashUsageTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashUsageTimer < 0)
        {
            dashUsageTimer = dashCooldown;
            dashDir = Input.GetAxisRaw("Horizontal");

            if (dashDir == 0)
            {
                dashDir = facingDir;
            }

            stateMachine.ChangesState(dashState);
        }
    }
    #region Velocity
    public void ZeroVelocity() => rb.velocity = new Vector2(0, 0);

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }
    #endregion

    #region Collision

    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    public bool IsHalfGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsHalfGround);
    public bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }

    #endregion

    #region Flip

    public void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    public void FlipController(float _x)
    {
        if (_x > 0 && !facingRight)
        {
            Flip();
        }
        else if (_x < 0 && facingRight)
        {
            Flip();
        }
    }

    #endregion
}

#region TUKAENAI
//public bool IsHalfGroundHeadHit() => Physics2D.Raycast(halfGroundCheck.position, Vector2.up, halfGroundCheckDistance, whatIsHalfGround);

//private void OnTriggerStay2D(Collider2D other)
//{
//    SwitchHalfGround(other);
//}
//private void OnTriggerEnter2D(Collider2D other)
//{
//    SwitchHalfGround(other);
//}

//private void SwitchHalfGround(Collider2D other)
//{
//    if (other.gameObject.CompareTag("HalfGround"))
//    {
//        Debug.Log("trigger halfground");
//        //頭から接触したら HalfGroundとプレイヤーのコリジョン無効化にする
//        if (IsHalfGroundHeadHit())
//        {
//            Debug.Log("trigger headhit");
//            //Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), other, true);
//            ToggleCollision(false);
//            dropFlag = false;
//        }
//        //脚から接触したら HalfGroundとプレイヤーのコリジョン有効にする
//        if (IsHalfGroundFootHit() && !dropFlag)
//        {
//            Debug.Log("trigger foothit");
//            //Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), other, false);
//            ToggleCollision(true);
//        }
//    }
//}

//private void ToggleCollision(bool enableCollision)
//{

//    int newLayer = enableCollision ? LayerMask.NameToLayer("Default") : LayerMask.NameToLayer("InCrossingHalfGround");
//    gameObject.layer = newLayer;
//}

//if (Input.GetKeyDown(KeyCode.Space) && (Input.GetKey(KeyCode.DownArrow)) && IsHalfGroundFootHit())
//{
//    Collider2D[] collidersArray = new Collider2D[1];
//    ContactFilter2D contactFilter = new();
//    Collider2D playerCollider = this.GetComponent<Collider2D>();
//    Physics2D.OverlapCollider(playerCollider, contactFilter, collidersArray);
//    if (collidersArray[0])
//    {
//        //Physics2D.IgnoreCollision(collidersArray[0], playerCollider, true);
//        ToggleCollision(false);
//        dropFlag = true;
//    }
//}
#endregion