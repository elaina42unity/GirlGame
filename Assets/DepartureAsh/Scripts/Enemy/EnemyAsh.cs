using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAsh : EntityAsh
{
    [SerializeField]private LayerMask whatIsPlayer_;

    [Header("Stunned Info")]
    public float stunDuration_;
    public Vector2 stunDirection_;
    protected bool canBeStunned_;
    [SerializeField] protected GameObject counterImage_;
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

    public virtual void OpenCounterAttackWindow()
    {
        canBeStunned_ = true;
        counterImage_.SetActive(true);
    }

    public virtual void CloseCounterAttackWindow()
    {
        canBeStunned_ = false;
        counterImage_.SetActive(false);
    }

    protected virtual bool CanBeStunned()
    {
        if (canBeStunned_)
        {
            CloseCounterAttackWindow();
            return true;
        }
        return false;
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
