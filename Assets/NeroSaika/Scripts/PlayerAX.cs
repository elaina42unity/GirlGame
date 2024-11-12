using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAX : MonoBehaviour
{
    public PlayerStateMachineAX stateMachine {  get; private set; }

    public PlayerIdleStateAX idleState { get; private set; }

    public PlayerMoveStateAX moveState { get; private set; }

    private void Awake()
    {
        stateMachine = new PlayerStateMachineAX();

        idleState = new PlayerIdleStateAX(this, stateMachine, "Idle");
        moveState = new PlayerMoveStateAX(this, stateMachine, "Move");
    }

    private void Start()
    {
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        stateMachine.currentState.Update();
    }
}
