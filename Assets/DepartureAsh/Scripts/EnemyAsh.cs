using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAsh : EntityAsh
{
    [SerializeField]public LayerMask whatIsPlayer_;
    [Header("Move info")]
    public float moveSpeed_;
    public float idleTime_;
    public float battleTime_;
    [Header("Attack info")]
    public float attackDistance_;
    public float attackCooldown_;
    [HideInInspector]public float lastTimeAttacked_;
     
    public EnemyStateMachineAsh StateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        StateMachine = new EnemyStateMachineAsh();
    }

    protected override void Update()
    {
        base.Update();
        StateMachine.CurrentState.Update();

    }

    public virtual void AnimationFinishTrigger()=>StateMachine.CurrentState.AnimationFinishTrigger();

    public virtual RaycastHit2D IsPlayerDetected()=> Physics2D.Raycast(wallCheck_.position,Vector2.right*FacingDir,50,whatIsPlayer_);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position .x+ attackDistance_ * FacingDir, transform.position.y));
    }
}
