using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_F : EntityFate
{
    [Header("Attack details")]
    public Vector2[] attackMovement;

    public bool isBusy { get; private set; }
    [Header("Move info")]
    public float moveSpeed = 8.0f;
    public float jumpForce;

    [Header("Dash Info")]
    [SerializeField] private float dashCooldown;
    private float dashUsageTimer;
    public float dashSpeed;
    public float dashDuration;
    public float dashDir { get; private set; }

    #region States
    public PlayerStateMachine_F stateMachine { get; private set; }

    public PlayerIdleState_F idleState { get; private set; }
    public PlayerMoveState_F moveState { get; private set; }
    public PlayerJumpState_F jumpState { get; private set; }
    public PlayerAirState_F airState { get; private set; }
    public PlayerWallSlideState_F wallSlide { get; private set; }
    public PlayerWallJumpState_F wallJump { get; private set; }
    public PlayerDashState_F dashState { get; private set; }

    public PlayerPrimaryAttackState_F primaryAttack { get; private set; }

    #endregion



    protected override void Awake()
    {
        base.Awake();
        stateMachine = new PlayerStateMachine_F();

        idleState = new PlayerIdleState_F(this, stateMachine, "Idle");
        moveState = new PlayerMoveState_F(this, stateMachine, "Move");
        jumpState = new PlayerJumpState_F(this, stateMachine, "Jump");
        airState = new PlayerAirState_F(this, stateMachine, "Jump");
        dashState = new PlayerDashState_F(this, stateMachine, "Dash");
        wallSlide = new PlayerWallSlideState_F(this, stateMachine, "WallSlide");
        wallJump = new PlayerWallJumpState_F(this, stateMachine, "WallJump");
        primaryAttack = new PlayerPrimaryAttackState_F(this, stateMachine, "Attack");


    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);

    }

    public float timer;
    public float cooldown = 5;


    protected override void Update()
    {
        base.Update();
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
        if (IsWallDected())
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

            stateMachine.ChangeState(dashState);

        }
    }






}
