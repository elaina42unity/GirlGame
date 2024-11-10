using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    #region LayerMask
    [SerializeField] private LayerMask whatIsPlayer_;
    #endregion
    [Header("Move info")]
    public float moveSpeed_;
    public float idleTime_;
    public float battleTime_;

    public EnemyStateMachineAsh StateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        StateMachine = new EnemyStateMachineAsh();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
}
