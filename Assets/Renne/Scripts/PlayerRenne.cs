using UnityEngine;

public class PlayerRenne : MonoBehaviour
{
    [Header("Move info")]
    public float moveSpeed;
    public float jumpForce;
    #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    #endregion
    #region States

    public PlayerStateMachineRenne stateMachine { get; private set; }

    public PlayerIdleStateRenne idleState { get; private set; }
    public PlayerMoveStateRenne moveState { get; private set; }
    public PlayerJumpStateRenne jumpState { get; private set; }
    public PlayerAirStateRenne airState { get; private set; }
    #endregion

    private void Awake()
    {
        stateMachine = new PlayerStateMachineRenne();

        idleState = new PlayerIdleStateRenne(this, stateMachine, "Idle");
        moveState = new PlayerMoveStateRenne(this, stateMachine, "Move");
        jumpState = new PlayerJumpStateRenne(this, stateMachine, "Jump");
        airState  = new PlayerAirStateRenne(this, stateMachine, "Jump");
    }

    public void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        stateMachine.currentState.Update();
    }

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
    }
}
