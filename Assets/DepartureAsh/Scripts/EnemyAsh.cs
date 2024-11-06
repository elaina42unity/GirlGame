using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAsh : MonoBehaviour
{
    public Rigidbody2D Rb { get; private set; }
    public Animator Anim { get; private set; }
    public EnemyStateMachineAsh StateMachine { get; private set; }

    private void Awake()
    {
        StateMachine = new EnemyStateMachineAsh();
    }

    private void Update()
    {
        StateMachine.CurrentState.Update();
    }
}
