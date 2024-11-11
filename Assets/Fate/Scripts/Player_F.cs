using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_F : MonoBehaviour
{
    [Header("Move info")]
    public float moveSpeed = 8.0f;

    #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }

    #endregion

    #region States
    public PlayerStateMachine_F stateMachine { get; private set; }

    public PlayerIdleState_F idleState { get; private set; }
    public PlayerMoveState_F moveState { get; private set; }
    #endregion



    private void Awake()
    {
        stateMachine = new PlayerStateMachine_F();

        idleState = new PlayerIdleState_F(this, stateMachine, "Idle");
        moveState = new PlayerMoveState_F(this, stateMachine, "Move");
    }

    private void Start()
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
