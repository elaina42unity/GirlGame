using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : Entity
{
    //variables
    [Header("Attack details")]
    public Vector2[] attackMovement;
    //public float counterAttackDuration = .2f;

    public bool isBusy {  get; private set; }
    [Header("Move info")]
    public float moveSpeed = 12f;
    public float jumpForce;
    //public float waterBallReturnImpact;

    [Header("Dash info")]
    public float dashSpeed;
    public float dashDuration;
    public float dashDir { get; private set; }

    [Header("BombFlash info")]
    public float bombFlashSpeed;
    public float bombFlashDuration;
    public float bombFlashDir { get; private set; }


    [Header("Chant info")]
    [SerializeField] private float chantCooldown;
    public float chantUsageTimer;
    public float chantSpeed;
    public float chantDuration;

    //States
    public float chantDir { get; private set; } 
    //public GameObject waterBall { get; private set; }

    public SkillManager skill {  get; private set; }

    #region States
    public PlayerStateMachine stateMachine {  get; private set; }

    public PlayerIdleState idleState { get; private set; }

    public PlayerMoveState moveState { get; private set; }

    public PlayerJumpState jumpState { get; private set; }

    public PlayerAirState airState { get; private set; }

    //public PlayerWallSlideState wallSlide { get; private set; }

    //public PlayerWallJumpState wallJump { get; private set; }

    public PlayerDashState dashState { get; private set; }

    //public PlayerChantStateAX chantState { get; private set; }

    public PlayerPrimaryAttackState primaryAttack { get; private set; }

    //public PlayerEnchantState enchant{ get; private set; }

    //public PlayerCounterAttackState counterAttack { get; private set; }

    //public PlayerBombFlashState bombFlashState { get; private set; }

    //public PlayerAimState aimState { get; private set; }

    //public PlayerCatchState catchState { get; private set; }

    //public PlayerBlackholeState blackHole {  get; private set; }

    public PlayerDeadState deadState { get; private set; }
    #endregion

<<<<<<< HEAD
    //initialize states
=======
    IInteractable targetPortal;


>>>>>>> 9910a27f004a15ee7488f8eb587716bea96c9d98
    protected override void Awake()
    {
        base.Awake();

        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        //wallSlide = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        //chantState = new PlayerChantState(this, stateMachine, "ChantAttack");
        //wallJump = new PlayerWallJumpState(this, stateMachine, "Jump");
        primaryAttack = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
        //enchant = new PlayerEnchantState(this, stateMachine, "ChantAttack");
        //counterAttack = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");
        //bombFlashState = new PlayerBombFlashState(this, stateMachine, "BombFlash");
        //aimState = new PlayerAimState(this, stateMachine, "Aim");
        //catchState = new PlayerCatchState(this, stateMachine, "Catch");
        //blackHole = new PlayerBlackholeState(this, stateMachine, "ChantAttack");
        deadState = new PlayerDeadState(this, stateMachine, "Die");

    }

    //initialize the first states and skill
    protected override void Start()
    {
        base.Start();

        skill = SkillManager.instance;

        stateMachine.Initialize(idleState);
    }

    
    protected override void Update()
    {
        base.Update();

        //Update the current state
        stateMachine.currentState.Update();

        //Some normal skills
        //#region
        //CheckforDashInput();        

        //CheckforBombFlashInput();

        //if (Input.GetKeyDown(KeyCode.G))            
        //    skill.staffMagic.CanUseSkill();

        //if (Input.GetKeyDown(KeyCode.F))
        //    skill.starMagic.CanUseSkill();
        //#endregion
    }

<<<<<<< HEAD

    ////skill setup
    //public void AssignNewWaterBall(GameObject _newWaterBall)
    //{
    //    waterBall = _newWaterBall;
    //}

    ////skill setup
    //public void CatchTheWaterBall() 
    //{ 
    //    stateMachine.ChangeState(catchState);
    //    Destroy(waterBall);
    //}

    //make player not to move between combo
    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;

        yield return new WaitForSeconds(_seconds);

        isBusy = false; 
    }

    //Set the animation event to stop the animation
    public void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    //input check
    //#region check
    //private void CheckforDashInput()
    //{
    //    if (IsWallDetected())
    //        return;

    //    if (Input.GetKeyDown(KeyCode.LeftShift) && SkillManagerAX.instance.dash.CanUseSkill() )
    //    {
    //        dashDir = Input.GetAxisRaw("Horizontal");

    //        if (dashDir == 0)
    //            dashDir = facingDir;

    //        stateMachine.ChangeState(dashState);
    //    }
    //}

    //private void CheckforBombFlashInput()
    //{

    //    if (Input.GetKeyDown(KeyCode.V) && SkillManagerAX.instance.dash.CanUseSkill())
    //    {
    //        bombFlashDir = Input.GetAxisRaw("Horizontal");

    //        if (bombFlashDir == 0)
    //            bombFlashDir = facingDir;

    //        stateMachine.ChangeState(bombFlashState);
    //    }
    //}

    //public void CheckforChantInput()
    //{


    //    if (Input.GetKeyDown(KeyCode.Z) && chantUsageTimer < 0 && groundCheck)
    //    {
    //        chantUsageTimer = chantCooldown;
    //        chantDir = Input.GetAxisRaw("Horizontal");

    //        if (chantDir == 0)
    //            chantDir = facingDir;

    //        stateMachine.ChangeState(chantState);
    //    }
    //}
    //#endregion 

    public override void Die()
    {
        base.Die();

        stateMachine.ChangeState(deadState);
    }




=======
    private void OnTriggerStay2D(Collider2D other)
    {
        //シーンの切り替え
        if (other.CompareTag("SwitchRoom"))
        {
            targetPortal = other.GetComponent<IInteractable>();
            targetPortal.ChangeRoom();
        }
    }
>>>>>>> 9910a27f004a15ee7488f8eb587716bea96c9d98
}
