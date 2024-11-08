using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [Header("Attack details")]
    public Vector2[] attackMovement_;
    public bool IsBusy { get; private set; }
    [Header("Move info")]
    public float moveSpeed_ = 12f;
    public float jumpForce_;

    [Header("Dash info")]
    [SerializeField] private float dashCooldown_;
    private float dashUsageTime_;
    public float dashSpeed_;
    public float dashDuration_;
    public float DashDir { get; private set; }

    #region States
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    #endregion


    protected override void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, "Idle");
        MoveState = new PlayerMoveState(this, StateMachine, "Move");

    }

    protected override void Start()
    {
        base.Start();
        StateMachine.Initialize(IdleState);
    }

    protected override void Update()
    {
        base.Update();
        StateMachine.CurrentState.Update();
    }
  
}
