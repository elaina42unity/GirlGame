using System.Collections;
using UnityEngine;

public class Player : Entity
{
    //variables

    [Header("Collider Damage")]
    public Transform colliderDamageCheck;
    public float cdDamageCheckWidth;
    public float cdDamageCheckHeight;

    [Header("Attack details")]
    public Vector2[] attackMovement;
    public float counterAttackDuration = .2f;
    public float counterAttackCheckWidth;
    public float counterAttackCheckHeight;
    public Transform counterAttackCheck;

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

    public SkillManager skill { get; private set; }

    #region States
    public PlayerStateMachine stateMachine { get; private set; }

    public PlayerIdleState idleState { get; private set; }

    public PlayerMoveState moveState { get; private set; }

    public PlayerJumpState jumpState { get; private set; }

    public PlayerAirState airState { get; private set; }

    //public PlayerWallSlideState wallSlide { get; private set; }

    //public PlayerWallJumpState wallJump { get; private set; }

    public PlayerDashState dashState { get; private set; }

    //public PlayerChantState chantState { get; private set; }

    public PlayerPrimaryAttackState primaryAttack { get; private set; }

    //public PlayerEnchantState enchant { get; private set; }

    public PlayerCounterAttackState counterAttack { get; private set; }

    public PlayerBombFlashState bombFlashState { get; private set; }

    public PlayerAimState aimState { get; private set; }

    //public PlayerCatchState catchState { get; private set; }

    public PlayerBlackholeState blackHole { get; private set; }

    public PlayerDeadState deadState { get; private set; }
    #endregion

    //initialize states
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
        counterAttack = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");
        bombFlashState = new PlayerBombFlashState(this, stateMachine, "BombFlash");
        aimState = new PlayerAimState(this, stateMachine, "Aim");
        //catchState = new PlayerCatchState(this, stateMachine, "Catch");
        blackHole = new PlayerBlackholeState(this, stateMachine, "ChantAttack");
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
        #region

        if (Input.GetAxisRaw("Horizontal") != 0)
            CheckforDashInput();

        if (Input.GetAxisRaw("Horizontal") == 0)
            CheckforBombFlashInput();

        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Joystick1Button2) && Input.GetAxisRaw("Vertical") >= 0)
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
        //stateMachine.ChangeState(catchState);
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

        if ((Input.GetKeyDown(KeyCode.LeftShift) && SkillManager.instance.dash.CanUseSkill())
            || (Input.GetKeyDown(KeyCode.Joystick1Button5) && SkillManager.instance.dash.CanUseSkill()))
        {
            dashDir = Input.GetAxisRaw("Horizontal");

            if (dashDir == 0)
                dashDir = facingDir;

            stateMachine.ChangeState(dashState);
        }
    }

    private void CheckforBombFlashInput()
    {

        if ((Input.GetKeyDown(KeyCode.LeftShift) && SkillManager.instance.dash.CanUseSkill())
            || (Input.GetKeyDown(KeyCode.Joystick1Button5) && SkillManager.instance.dash.CanUseSkill()))
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

            //stateMachine.ChangeState(chantState);
        }
    }
    #endregion 

    public override void Die()
    {
        base.Die();

        stateMachine.ChangeState(deadState);
    }

    public virtual void getEnemyColliderDamage()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(colliderDamageCheck.position, new Vector2(cdDamageCheckWidth, cdDamageCheckHeight), 0);
        foreach (var hit in colliders)
        {
            Debug.Log("2");
            if (hit.GetComponent<Enemy>() != null)
            {
                stats.TakeDamage(4);
            }
        }
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawCube(counterAttackCheck.position, new Vector3(counterAttackCheckHeight, counterAttackCheckWidth, 0));
        Gizmos.DrawCube(colliderDamageCheck.position, new Vector3(cdDamageCheckWidth, cdDamageCheckHeight, 0));

    }

    protected override IEnumerator HitKnockback()
    {
        return base.HitKnockback();
    }
}
