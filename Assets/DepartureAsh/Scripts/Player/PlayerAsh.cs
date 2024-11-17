using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAsh : EntityAsh
{

    [Header("Attack details")]
    public Vector2[] attackMovement_;
    public float counterAttackDuration_=0.2f;
    public bool IsBusy { get;private set; }
    [Header("Move info")]
    public float moveSpeed_ = 12f;
    public float jumpForce_;

    [Header("Dash info")]
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
    public PlayerCounterAttackStateAsh CounterAttackState { get; private set; }
    #endregion

    public SkillManagerAsh SkillManager { get; private set; }

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
        CounterAttackState = new PlayerCounterAttackStateAsh(this, StateMachine,"CounterAttack");
    }

    protected override void Start()
    {
        base.Start();

        SkillManager = SkillManagerAsh.instance_;

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

        if (Input.GetKeyDown(KeyCode.LeftShift) && SkillManagerAsh.instance_.Dash.CanUseSkill())
        {
            
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
