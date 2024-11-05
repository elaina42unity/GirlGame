using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAsh : EntityAsh
{

    [Header("Attack details")]
    public Vector2[] attackMovement_;
    public bool IsBusy { get;private set; }
    [Header("Move info")]
    public float moveSpeed_ = 12f;
    public float jumpForce_;

    [Header("Dash info")]
    [SerializeField] private float dashCooldown_;
    private float dashUsageTime_;
    public float dashSpeed_;
    public float dashDuration_;
    public float DashDir { get; private set; }

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


    protected override void Awake()
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

    protected override void Start()
    {
        base.Start();
        StateMachine.Initialize(IdleState);
    }

    protected override void Update()
    {
        base.Update(); 
        StateMachine.CurrentState.Update();

        CheckForDashInput();
    }

    public IEnumerator BusyFor(float seconds)
    {
        IsBusy = true;

        yield return new WaitForSeconds(seconds);

        IsBusy = false;
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
                DashDir = FacingDir;

            // 如果没有输入方向就不进行冲刺的处理方法
            //if (DashDir!=0)
            //    StateMachine.ChangeState(DashState);

            StateMachine.ChangeState(DashState);
        }
    }


}
