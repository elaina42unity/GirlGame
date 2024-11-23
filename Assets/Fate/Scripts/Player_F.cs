using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_F : MonoBehaviour
{
    [Header("Move info")]
    public float moveSpeed = 8.0f;
    public float jumpForce;

    [Header("Collision info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;

    public int facingDir { get; private set; } = 1;
    private bool facingRight = true;

    #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }

    #endregion

    #region States
    public PlayerStateMachine_F stateMachine { get; private set; }

    public PlayerIdleState_F idleState { get; private set; }
    public PlayerMoveState_F moveState { get; private set; }
    public PlayerJumpState_F jumpState { get; private set; }    
    public PlayerAirState_F airState { get; private set; }  

    #endregion



    private void Awake()
    {
        stateMachine = new PlayerStateMachine_F();

        idleState = new PlayerIdleState_F(this, stateMachine, "Idle");
        moveState = new PlayerMoveState_F(this, stateMachine, "Move");
        jumpState = new PlayerJumpState_F(this, stateMachine, "Jump");
        airState = new PlayerAirState_F(this, stateMachine, "Jump");
    }

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        stateMachine.Initialize(idleState);

    }

    private void Update()
    {
        stateMachine.currentState.Update();
        
    }

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }

    public bool IsGroundDected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance,whatIsGround);


    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }

    public void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0,180,0);
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

}
