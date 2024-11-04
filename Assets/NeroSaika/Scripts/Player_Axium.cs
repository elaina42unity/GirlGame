using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Player_Axium : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpforce;
    [SerializeField] private float dashSpeed;

    [Header("Collision info")]
    [SerializeField] private float dashtime;
    [SerializeField] private float dashDuration;

    [SerializeField] private float dashCooldown;
    private float dashCooldownTimer;

    private float xInput;
    private float yInput;

    private int facingDir = 1;
    private bool facingRight = true;

    private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        CheckInput();
        CollisionChecks();

        dashtime -= Time.deltaTime;
        dashCooldownTimer -= Time.deltaTime;
        
        flipController();
        AnimatorCotroller();
    }

    private void CheckInput()
    {
        xInput = UnityEngine.Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DashAbility();
        }

        if (UnityEngine.Input.GetKeyDown(KeyCode.UpArrow))
        {
            // プレイヤーを上に移動させる
            Jump();
        }
    }

    private void DashAbility()
    {
        if (dashCooldownTimer < 0) 
        {
            dashCooldownTimer = dashCooldown;
            dashtime = dashDuration;
        }
    }

    private void CollisionChecks()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    }

    private void Movement()
    {
        if(dashtime> 0)
        {
            rb.velocity = new Vector2(xInput * dashSpeed, 0);
        }
        else
        {
            rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
        }
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
        }
    }

    private void AnimatorCotroller()
    {
        bool isMoving = rb.velocity.x != 0;

        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isDashing", dashtime > 0);
    }

    private void flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void flipController()
    {
        if (rb.velocity.x > 0 && !facingRight)
        {
            flip();
        }else if(rb.velocity.x < 0 && facingRight)
        {
            flip();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x,transform.position.y-groundCheckDistance));
    }
}
