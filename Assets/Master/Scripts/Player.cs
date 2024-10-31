using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region common variable space
    // ----------------------------------------- common variable space -----------------------------------------
    // Player's state
    private enum State
    {
        NONE = -1,
        GROUND,
        ATTACK,
        AIR,
        DASHING
    }

    // State Variables  
    private State _currentState;
    private State _nextState;

    // Variables for the component
    private Rigidbody2D _rb; // rigidbody
    private Animator _anim; // Animator

    //  state machine input variables
    private bool isAttacking;
    private bool isAir;
    private bool isDashing;
    private bool isGround;
    #endregion
    #region free variable space
    // ----------------------------------------- free variable space -----------------------------------------
    // TODO: variable workspace free to use as needed


    #endregion
    #region Fate variable space
    // ----------------------------------------- Fate variable space -----------------------------------------
    // TODO: Fate's variable workspace

    #endregion

    #region Ash variable space
    // ----------------------------------------- Ash variable space -----------------------------------------
    // TODO: Ash's variable workspace

    #endregion

    #region NeroSaika variable space
    // ----------------------------------------- NeroSaika variable space -----------------------------------------
    // TODO: NeroSaika's variable workspace

    #endregion

    #region Renne variable space
    // ----------------------------------------- Renne variable space -----------------------------------------
    // TODO: Renne's variable workspace

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // Component initialization
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponentInChildren<Animator>();

        // State initialization
        _currentState = State.GROUND;
        _nextState = _currentState;
    }

    // Update is called once per frame
    void Update()
    {


        if (_nextState != _currentState)
        {
            // TODO: Handle transition states


            // State transition
            _currentState = _nextState;
        }
        else // Handle when in a stable state
        {
            switch (_currentState)
            {
                case State.GROUND:
                    if (!CheckTransitionFromCurrentState(ref _nextState, _currentState))
                        GroundState();
                    break;
                case State.ATTACK:
                    if (!CheckTransitionFromCurrentState(ref _nextState, _currentState))
                        AttackState();
                    break;
                case State.AIR:
                    if (!CheckTransitionFromCurrentState(ref _nextState, _currentState))
                        AirState();
                    break;
                case State.DASHING:
                    if (!CheckTransitionFromCurrentState(ref _nextState, _currentState))
                        DashingState();
                    break;
            }
        }
    }

    #region common function space
    // ----------------------------------------- common function space -----------------------------------------
    private bool CheckTransitionFromCurrentState(ref State next, State current)
    {
        switch (current)
        {
            case State.GROUND:
                return CheckTransitionFromGround(ref next, current);
            case State.ATTACK:
                return CheckTransitionFromAttack(ref next, current);
            case State.AIR:
                return CheckTransitionFromAir(ref next, current);
            case State.DASHING:
                return CheckTransitionFromDashing(ref next, current);
            default:
                Debug.LogError("CheckExitCondition(): " + _nextState + " State is not being handled");
                return false;
        }
    }

    private bool CheckTransitionFromGround(ref State next, State current)
    {
        if (isAttacking)
        {
            next = State.ATTACK;
            return true;
        }
        else if (isAir)
        {
            next = State.AIR;
            return true;
        }
        return false;
    }

    private bool CheckTransitionFromAttack(ref State next, State current)
    {
        if (!isAttacking)
        {
            next = State.GROUND;
            return true;
        }
        return false;
    }

    private bool CheckTransitionFromAir(ref State next, State current)
    {
        if (!isAir)
        {
            next = State.GROUND;
            return true;
        }
        else if (isDashing)
        {
            next = State.DASHING;
            return true;
        }
        return false;
    }

    private bool CheckTransitionFromDashing(ref State next, State current)
    {
        if (!isDashing && isGround)
        {
            next = State.GROUND;
            return true;
        }
        else if (!isDashing && !isGround)
        {
            next = State.AIR;
            return true;
        }
        return false;
    }
    #endregion

    #region Fate function space
    // ----------------------------------------- Fate function space -----------------------------------------
    // TODO: Fate's function workspace
    private void AirState()
    {

    }

    #endregion

    #region Ash function space
    // ----------------------------------------- Ash function space -----------------------------------------
    // TODO: Ash's function workspace
    private void GroundState()
    {

    }

    #endregion

    #region NeroSaika function space
    // ----------------------------------------- NeroSaika function space -----------------------------------------
    // TODO: NeroSaika's function workspace
    private void AttackState()
    {

    }

    #endregion

    #region Renne function space
    // ----------------------------------------- Renne function space -----------------------------------------
    // TODO: Renne's function workspace
    private void DashingState()
    {

    }

    #endregion
}
