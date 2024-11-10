using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : State
{
    public Enemy EnemyObject
    {
        get { return base.Entity as Enemy; }
        protected set { base.Entity = value; }
    }

    public EnemyState(Entity entity, StateMachine stateMachine, string animBoolName) : base(entity, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void CopyInfoFromOtherState(State otherState)
    {
        
    }
}
