using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAX : MonoBehaviour
{
    #region Components
    public Animator anim {  get; private set; }

    #endregion


    #region States
    public PlayerStateMachineAX stateMachine {  get; private set; }

    public PlayerIdleStateAX idleState { get; private set; }

    public PlayerMoveStateAX moveState { get; private set; }

    #endregion


    private void Awake()
    {
        stateMachine = new PlayerStateMachineAX();

        idleState = new PlayerIdleStateAX(this, stateMachine, "Idle");
        moveState = new PlayerMoveStateAX(this, stateMachine, "Move");
    }

    private void Start()
    {
        anim=GetComponentInChildren<Animator>();

        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        stateMachine.currentState.Update();
    }
}
