using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Player_Axium : Entity_Axium
{

    [Header("Move info")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpforce;
    [SerializeField] private float dashSpeed;

    private float xInput;
    private float yInput;

    [Header("Collision info")]
    [SerializeField] private float dashtime;
    [SerializeField] private float dashDuration;

    [Header("Attack info")]
    private bool isAttacking;
    private int comboCounter;
    [SerializeField]private float comboTime = .3f;
    private float comboTimeWindow;

    [Header("Dash info")]
    [SerializeField] private float dashCooldown;
    private float dashCooldownTimer;

    // Start is called before the first frame update
    protected override void Start()
    {

        base.Start();

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Start();

        Movement();
        CheckInput();
        CollisionChecks();

        dashtime -= Time.deltaTime;
        dashCooldownTimer -= Time.deltaTime;
        comboTimeWindow -= Time.deltaTime;

        flipController();
        AnimatorCotroller();
    }

    //The Attack over function 
    public void AttackOver()
    {
        isAttacking = false;

        comboCounter++;

        if( comboCounter > 2 ) 
            comboCounter = 0;

    }

    //Check the buttom been pressed
    private void CheckInput()
    {
        xInput = UnityEngine.Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartAttackEvent();
        }

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

    //Make an attack start
    private void StartAttackEvent()
    {
        if (!isGrounded)
            return;

        if (comboTimeWindow < 0)
            comboTimeWindow = 0;

        isAttacking = true;
        comboTimeWindow = comboTime;
    }

    //Make a dash operation
    private void DashAbility()
    {
        if (dashCooldownTimer < 0 && !isAttacking) 
        {
            dashCooldownTimer = dashCooldown;
            dashtime = dashDuration;
        }
    }

    //Make the movement
    private void Movement()
    {
        if (isAttacking)
        {
            rb.velocity = new Vector2(0, 0);
        }else if (dashtime> 0)
        {
            rb.velocity = new Vector2(facingDir * dashSpeed, 0);
        }
        else
        {
            rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
        }
    }

    //Make a jump operation
    private void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
        }
    }

    //Control the animation to unity
    private void AnimatorCotroller()
    {
        bool isMoving = rb.velocity.x != 0;

        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isDashing", dashtime > 0);
        anim.SetBool("isAttacking", isAttacking);
        anim.SetInteger("comboCounter", comboCounter);
    }

    //To Countrol the character's facing direction
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

}
