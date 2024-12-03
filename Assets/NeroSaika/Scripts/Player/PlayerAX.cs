using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAX : EntityAX
{
    [Header("Attack details")]
    public Vector2[] attackMovement;


    public bool isBusy {  get; private set; }
    [Header("Move info")]
    public float moveSpeed = 12f;
    public float jumpForce;

    [Header("Dash info")]
    [SerializeField] private float dashCooldown;
    private float dashUsageTimer;
    public float dashSpeed;
    public float dashDuration;
    public float dashDir {  get; private set; }

    

    [Header("Dash info")]
    [SerializeField] private float chantCooldown;
    public float chantUsageTimer;
    //public float dashSpeed;
    public float chantDuration;
    public float chantDir { get; private set; }


    

    

    #region States
    public PlayerStateMachineAX stateMachine {  get; private set; }

    public PlayerIdleStateAX idleState { get; private set; }

    public PlayerMoveStateAX moveState { get; private set; }

    public PlayerJumpStateAX jumpState { get; private set; }

    public PlayerAirStateAX airState { get; private set; }

    public PlayerWallSlideStateAX wallSlide { get; private set; }

    public PlayerWallJumpStateAX wallJump { get; private set; }

    public PlayerDashStateAX dashState { get; private set; }

    public PlayerChantStateAX chantState { get; private set; }

    public PlayerPrimaryAttackStateAX primaryAttack { get; private set; }

    public PlayerEnchantStateAX enchant{ get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        stateMachine = new PlayerStateMachineAX();

        idleState = new PlayerIdleStateAX(this, stateMachine, "Idle");
        moveState = new PlayerMoveStateAX(this, stateMachine, "Move");
        jumpState = new PlayerJumpStateAX(this, stateMachine, "Jump");
        airState = new PlayerAirStateAX(this, stateMachine, "Jump");
        dashState = new PlayerDashStateAX(this, stateMachine, "Dash");
        wallSlide = new PlayerWallSlideStateAX(this, stateMachine, "WallSlide");
        chantState = new PlayerChantStateAX(this, stateMachine, "ChantAttack");
        wallJump = new PlayerWallJumpStateAX(this, stateMachine, "Jump");
        primaryAttack = new PlayerPrimaryAttackStateAX(this, stateMachine, "Attack");
        enchant = new PlayerEnchantStateAX(this, stateMachine, "ChantAttack");
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();

        stateMachine.currentState.Update();

        CheckforDashInput();

    }

    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;

        yield return new WaitForSeconds(_seconds);

        isBusy = false; 
    }

    public void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    #region check
    private void CheckforDashInput()
    {
        if (IsWallDetected())
            return;

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
        

        if (Input.GetKeyDown(KeyCode.Z) && chantUsageTimer < 0 && groundCheck)
        {
            chantUsageTimer = chantCooldown;
            chantDir = Input.GetAxisRaw("Horizontal");

            if (chantDir == 0)
                chantDir = facingDir;

            stateMachine.ChangeState(chantState);
        }
    }
    #endregion 

    

    

    
}
