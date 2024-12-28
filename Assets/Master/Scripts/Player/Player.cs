using System.Collections;
using UnityEngine;
using Cinemachine;
using UnityEngine.UIElements;
using System;

public class Player : Entity
{
    public bool isdead = false;

    [SerializeField]private bool turnCamera = false;
    [SerializeField]private bool turnCameraBack = false;
    [SerializeField]private float cameraBackTimer;
    //variables

    [Header("Collider Damage")]
    public Transform colliderDamageCheck;
    public float cdDamageCheckWidth;
    public float cdDamageCheckHeight;

    [Header("Attack details")]
    public Vector2[] attackMovement;
    public float counterAttackDuration;
    public float counterAttackCheckWidth;
    public float counterAttackCheckHeight;
    public Transform counterAttackCheck;

    public bool isBusy { get; private set; }
    [Header("Move info")]
    public float moveSpeed;
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
    public GameObject magicball { get; private set; }

    //States
    public float chantDir { get; private set; }

    public SkillManager skill { get; private set; }

    #region States
    public PlayerStateMachine stateMachine { get; private set; }

    public PlayerIdleState idleState { get; private set; }

    public PlayerMoveState moveState { get; private set; }

    public PlayerJumpState jumpState { get; private set; }

    public PlayerAirState airState { get; private set; }

    public PlayerDashState dashState { get; private set; }

    //public PlayerChantState chantState { get; private set; }

    public PlayerPrimaryAttackState primaryAttack { get; private set; }

    //public PlayerEnchantState enchant { get; private set; }

    public PlayerCounterAttackState counterAttack { get; private set; }

    public PlayerBombFlashState bombFlashState { get; private set; }

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
        //chantState = new PlayerChantState(this, stateMachine, "ChantAttack");
        primaryAttack = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
        //enchant = new PlayerEnchantState(this, stateMachine, "ChantAttack");
        counterAttack = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");
        bombFlashState = new PlayerBombFlashState(this, stateMachine, "BombFlash");
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

        //controller test
        for (int i = 0; i <= 19; i++)
        {
            KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), "Joystick1Button" + i);
            if (Input.GetKeyDown(key))
            {
                Debug.Log("Joystick 1 Button " + i + " is pressed!");
            }
        }

        //カメラ逆転する
        if (turnCamera)
        {
            TurnCamera();
        }
        //カメラ、重力を元に戻す
        if (turnCameraBack)
        {
            cameraBackTimer -= Time.deltaTime;
            if (cameraBackTimer <= 0f)
            {
                TurnCameraBack();
            }
        }

        if (Input.GetKeyDown(KeyCode.P) )
        {
            ChangeSpecialRoom();
        }

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

    //set the dead state
    public override void Die()
    {
        base.Die();

        if (!isdead)
            stateMachine.ChangeState(deadState);
    }

    //get the line and cube of attack
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawCube(counterAttackCheck.position, new Vector3(counterAttackCheckHeight, counterAttackCheckWidth, 0));

    }

    //set the knockback effect
    protected override IEnumerator HitKnockback()
    {
        return base.HitKnockback();
    }

     private void OnTriggerStay2D(Collider2D other)
    {
        //シーンの切り替え
        if (other.CompareTag("SwitchRoom"))
        {
            IInteractable targetPortal;
            targetPortal = other.GetComponent<IInteractable>();
            targetPortal.ChangeRoom();
        }
    }

    public void ChangeSpecialRoom()
    {
        //カメラの逆転を開始する
        turnCamera = true;
    }

    //カメラを逆転する
    public void TurnCamera()
    {
        var camObj = GameObject.Find("Virtual Camera");
        CinemachineVirtualCamera cinemachine = camObj.GetComponent<CinemachineVirtualCamera>();
        //カメラlensを-14まで減る
        if (cinemachine.m_Lens.OrthographicSize <= -14)
        {
            turnCamera = false;
            //重力を逆転する
            Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
            rigidbody2D.gravityScale = -2;
            transform.localScale = new Vector3(1,-1,0);

            //重力戻すタイマーを起動
            turnCameraBack = true;

            return;
        }
        cinemachine.m_Lens.OrthographicSize -= Time.deltaTime * 5;
    }

    //カメラ、重力、Scaleを元へ戻す
    private void TurnCameraBack()
    {
        turnCameraBack = false;

        Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.gravityScale = 22;
        transform.localScale = new Vector3(1,1,0);

        var camObj = GameObject.Find("Virtual Camera");
        CinemachineVirtualCamera cinemachine = camObj.GetComponent<CinemachineVirtualCamera>();
        cinemachine.m_Lens.OrthographicSize = 22;
    }
}
