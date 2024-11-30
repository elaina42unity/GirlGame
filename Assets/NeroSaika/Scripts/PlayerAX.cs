using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAX : MonoBehaviour
{
    [Header("Move info")]
    public float moveSpeed = 12f;
    public float jumpForce;
    #region Components
    public Animator anim {  get; private set; }

    public Rigidbody2D rb { get; private set; }

    #endregion


    #region States
    public PlayerStateMachineAX stateMachine {  get; private set; }

    public PlayerIdleStateAX idleState { get; private set; }

    public PlayerMoveStateAX moveState { get; private set; }

    public PlayerJumpStateAX jumpState { get; private set; }

    public PlayerAirStateAX airState { get; private set; }

    #endregion


    private void Awake()
    {
        stateMachine = new PlayerStateMachineAX();

        idleState = new PlayerIdleStateAX(this, stateMachine, "Idle");
        moveState = new PlayerMoveStateAX(this, stateMachine, "Move");
        jumpState = new PlayerJumpStateAX(this, stateMachine, "Jump");
        airState = new PlayerAirStateAX(this, stateMachine, "Jump");
    }

    private void Start()
    {
        anim=GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        stateMachine.currentState.Update();
    }

    public void SetVelocity(float _xVelocity,float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
    }
}
