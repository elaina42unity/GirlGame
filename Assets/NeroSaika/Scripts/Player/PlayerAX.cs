using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAX : EntityAX
{
    [Header("Attack details")]
    public Vector2[] attackMovement;
    public float counterAttackDuration = .2f;

    public bool isBusy {  get; private set; }
    [Header("Move info")]
    public float moveSpeed = 12f;
    public float jumpForce;

    [Header("Dash info")]
    public float dashSpeed;
    public float dashDuration;
    public float dashDir { get; private set; }

    [Header("BombFlash info")]
    public float bombFlashSpeed;
    public float bombFlashDuration;
    public float bombFlashDir { get; private set; }


    [Header("Dash info")]
    [SerializeField] private float chantCooldown;
    public float chantUsageTimer;
    //public float dashSpeed;
    public float chantDuration;
    public float chantDir { get; private set; } 
    public GameObject waterBall;

    public SkillManagerAX skill {  get; private set; }

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

    public PlayerCounterAttackStateAX counterAttack { get; private set; }

    public PlayerBombFlashStateAX bombFlashState { get; private set; }

    public PlayerAimStateAX aimState { get; private set; }

    public PlayerCatchStateAX catchState { get; private set; }
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
        counterAttack = new PlayerCounterAttackStateAX(this, stateMachine, "CounterAttack");
        bombFlashState = new PlayerBombFlashStateAX(this, stateMachine, "BombFlash");
        aimState = new PlayerAimStateAX(this, stateMachine, "Aim");
        catchState = new PlayerCatchStateAX(this, stateMachine, "Catch");
    }

    protected override void Start()
    {
        base.Start();

        skill = SkillManagerAX.instance;

        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();

        stateMachine.currentState.Update();

        CheckforDashInput();

        CheckforBombFlashInput();

    }


    public void AssignNewWaterBall(GameObject _newWaterBall)
    {
        waterBall = _newWaterBall;
    }

    public void ClearTheWaterBall() 
    { 
        Destroy(waterBall);
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

        if (Input.GetKeyDown(KeyCode.LeftShift) && SkillManagerAX.instance.dash.CanUseSkill() )
        {
            dashDir = Input.GetAxisRaw("Horizontal");

            if (dashDir == 0)
                dashDir = facingDir;

            stateMachine.ChangeState(dashState);
        }
    }

    private void CheckforBombFlashInput()
    {

        if (Input.GetKeyDown(KeyCode.V) && SkillManagerAX.instance.dash.CanUseSkill())
        {
            bombFlashDir = Input.GetAxisRaw("Horizontal");

            if (bombFlashDir == 0)
                bombFlashDir = facingDir;

            stateMachine.ChangeState(bombFlashState);
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
