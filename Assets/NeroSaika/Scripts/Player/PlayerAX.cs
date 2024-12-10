using System.Collections;
using UnityEngine;

public class PlayerAX : EntityAX
{
    //variables
    [Header("Attack details")]
    public Vector2[] attackMovement;
    public float counterAttackDuration = .2f;

    public bool isBusy { get; private set; }
    [Header("Move info")]
    public float moveSpeed = 12f;
    public float jumpForce;
    public float waterBallReturnImpact;

    [Header("Dash info")]
    public float dashSpeed;
    public float dashDuration;
    public float dashDir { get; private set; }

    [Header("BombFlash info")]
    public float bombFlashSpeed;
    public float bombFlashDuration;
    public bool isdodging;
    public float bombFlashDir { get; private set; }


    [Header("Dash info")]
    [SerializeField] private float chantCooldown;
    public float chantUsageTimer;
    //public float dashSpeed;
    public float chantDuration;

    //States
    public float chantDir { get; private set; }
    public GameObject waterBall { get; private set; }

    public SkillManagerAX skill { get; private set; }

    #region States
    public PlayerStateMachineAX stateMachine { get; private set; }

    public PlayerIdleStateAX idleState { get; private set; }

    public PlayerMoveStateAX moveState { get; private set; }

    public PlayerJumpStateAX jumpState { get; private set; }

    public PlayerAirStateAX airState { get; private set; }

    public PlayerWallSlideStateAX wallSlide { get; private set; }

    public PlayerWallJumpStateAX wallJump { get; private set; }

    public PlayerDashStateAX dashState { get; private set; }

    public PlayerChantStateAX chantState { get; private set; }

    public PlayerPrimaryAttackStateAX primaryAttack { get; private set; }

    public PlayerEnchantStateAX enchant { get; private set; }

    public PlayerCounterAttackStateAX counterAttack { get; private set; }

    public PlayerBombFlashStateAX bombFlashState { get; private set; }

    public PlayerAimStateAX aimState { get; private set; }

    public PlayerCatchStateAX catchState { get; private set; }

    public PlayerBlackholeStateAX blackHole { get; private set; }

    public PlayerDeadStateAX deadState { get; private set; }
    #endregion

    //initialize states
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
        blackHole = new PlayerBlackholeStateAX(this, stateMachine, "ChantAttack");
        deadState = new PlayerDeadStateAX(this, stateMachine, "Die");

    }

    //initialize the first states and skill
    protected override void Start()
    {
        base.Start();

        skill = SkillManagerAX.instance;

        stateMachine.Initialize(idleState);
    }


    protected override void Update()
    {
        base.Update();

        //Update the current state
        stateMachine.currentState.Update();

        //Some normal skills
        #region

        if (Input.GetAxisRaw("Horizontal")!=0)
            CheckforDashInput();

        if (Input.GetAxisRaw("Horizontal") == 0)
            CheckforBombFlashInput();

        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Joystick1Button2) && Input.GetAxisRaw("Vertical")>=0 )
            skill.starMagic.CanUseSkill();
        #endregion

        for (int i = 0; i <= 19; i++)
        {
            KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Joystick1Button" + i);
            if (Input.GetKeyDown(key))
            {
                Debug.Log("Joystick 1 Button " + i + " is pressed!");
            }
        }

    }


    //skill setup
    public void AssignNewWaterBall(GameObject _newWaterBall)
    {
        waterBall = _newWaterBall;
    }

    //skill setup
    public void CatchTheWaterBall()
    {
        stateMachine.ChangeState(catchState);
        Destroy(waterBall);
    }

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
    #region check
    private void CheckforDashInput()
    {
        if (IsWallDetected())
            return;

        if ((Input.GetKeyDown(KeyCode.LeftShift) && SkillManagerAX.instance.dash.CanUseSkill())
            || (Input.GetKeyDown(KeyCode.Joystick1Button5) && SkillManagerAX.instance.dash.CanUseSkill()))
        {
            dashDir = Input.GetAxisRaw("Horizontal");

            if (dashDir == 0)
                dashDir = facingDir;

            stateMachine.ChangeState(dashState);
        }
    }

    private void CheckforBombFlashInput()
    {

        if ((Input.GetKeyDown(KeyCode.LeftShift) && SkillManagerAX.instance.dash.CanUseSkill())
            || (Input.GetKeyDown(KeyCode.Joystick1Button5) && SkillManagerAX.instance.dash.CanUseSkill()))
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

    public override void Die()
    {
        base.Die();

        stateMachine.ChangeState(deadState);
    }

    public virtual void getEnemyDamage()
    {

    }


}
