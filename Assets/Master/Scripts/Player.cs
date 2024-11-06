using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

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
    private State currentState_;
    private State nextState_;

    // Variables for the component
    private Rigidbody2D rb_; // rigidbody
    private Animator anim_; // Animator

    //  state machine input variables
    private bool isAttacking_ = false;
    private bool isAir_ = false;
    private bool isDashing_ = false;
    private bool isGround_ =true;

    private float xInput_; // input value of x axis
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
    private enum StateInGroundState
    {
        NONE = -1,
        IDLE,
        MOVE
    }

    private StateInGroundState currentGroundState_;
    private StateInGroundState nextGroundState_;

    [Header("Move Info")]
    [SerializeField]private float moveSpeed_ = 10f;

    private int facingDir_ = 1;
    private bool facingRight_ = true;
    #endregion

    #region NeroSaika variable space
    // ----------------------------------------- NeroSaika variable space -----------------------------------------
    // TODO: NeroSaika's variable workspace

    #endregion

    #region Renne variable space
    // ----------------------------------------- Renne variable space -----------------------------------------
    // TODO: Renne's variable workspace
    //combo番号 0～2
    private int comboCounter_ = 0;
    //comboを維持する時間
    private float comboTime_ = 1.0f;
    //comboを維持する時間の減算
    private float comboTimeWindow_;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // Component initialization
        rb_ = GetComponent<Rigidbody2D>();
        anim_ = GetComponentInChildren<Animator>();

        // State initialization
        currentState_ = State.GROUND;
        nextState_ = currentState_;

        currentGroundState_ = StateInGroundState.IDLE;
        nextGroundState_ = StateInGroundState.IDLE;
    }

    // Update is called once per frame
    void Update()
    {
        xInput_ = Input.GetAxisRaw("Horizontal");

        ComboTimeReduce();

        if (nextState_ != currentState_)
        {
            // TODO: Handle transition states
            if (nextState_ == State.GROUND)
            {
                currentGroundState_ = StateInGroundState.IDLE;
                nextGroundState_ = StateInGroundState.IDLE;
            }

            // State transition
            currentState_ = nextState_;
        }
        else // Handle when in a stable state
        {
            switch (currentState_)
            {
                case State.GROUND:
                    if (!CheckTransitionFromCurrentState(ref nextState_, currentState_))
                        GroundState();
                    break;
                case State.ATTACK:
                    if (!CheckTransitionFromCurrentState(ref nextState_, currentState_))
                        AttackState();
                    break;
                case State.AIR:
                    if (!CheckTransitionFromCurrentState(ref nextState_, currentState_))
                        AirState();
                    break;
                case State.DASHING:
                    if (!CheckTransitionFromCurrentState(ref nextState_, currentState_))
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
                Debug.LogError("CheckExitCondition(): " + nextState_ + " State is not being handled");
                return false;
        }
    }

    private bool CheckTransitionFromGround(ref State next, State current)
    {
        if (isAttacking_)
        {
            next = State.ATTACK;
            return true;
        }
        else if (isAir_)
        {
            next = State.AIR;
            return true;
        }
        else if (isDashing_)
        {
            next = State.DASHING;
            return true;
        }
        return false;
    }

    private bool CheckTransitionFromAttack(ref State next, State current)
    {
        if (!isAttacking_&&isGround_)
        {
            next = State.GROUND;
            return true;
        }
        return false;
    }

    private bool CheckTransitionFromAir(ref State next, State current)
    {
        if (!isAir_&&isGround_)
        {
            next = State.GROUND;
            return true;
        }
        else if (isDashing_)
        {
            next = State.DASHING;
            return true;
        }
        return false;
    }

    private bool CheckTransitionFromDashing(ref State next, State current)
    {
        if (!isDashing_ && isGround_)
        {
            next = State.GROUND;
            return true;
        }
        else if (!isDashing_ && isAir_)
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
        if (Input.GetKeyDown(KeyCode.J))
        {
            isAttacking_ = true;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isGround_ = false;
            isAir_ = true;
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isDashing_ = true;
            return;
        }

        if (nextGroundState_!=currentGroundState_)
        {

            currentGroundState_ = nextGroundState_;
        }
        else
        {
            switch (currentGroundState_)
            {
                case StateInGroundState.IDLE:
                    rb_.velocity = Vector3.zero;
                    if (xInput_!=0)
                    {
                       
                        nextGroundState_ = StateInGroundState.MOVE;
                    }
                    break;
                case StateInGroundState.MOVE:

                    FlipController(xInput_);
                    rb_.velocity = new Vector3(xInput_ * moveSpeed_, rb_.velocity.y);

                    if (xInput_ == 0)
                    {
                        nextGroundState_ = StateInGroundState.IDLE;
                    }
                    break;

            }
        }
    }

    private void FlipController(float x)
    {
        if (x > 0 && !facingRight_)
        {
            Flip();
        }
        else if (x < 0 && facingRight_)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingDir_ = facingDir_ * -1;
        facingRight_ = !facingRight_;
        transform.Rotate(0, 180, 0);
    }

    #endregion


    #region Renne function space
    // ----------------------------------------- NeroSaika function space -----------------------------------------
    // TODO: Renne's function workspace
    private void AttackState()
    {
        //維持する時間に経ったらcombo数をリセット
        if (comboTimeWindow_ < 0)
        {
            comboCounter_ = 0;
        }

        comboTimeWindow_ = comboTime_;
    }

    private void ComboTimeReduce()
    {
        comboTimeWindow_ -= Time.deltaTime;
    }

    //アニメーターイベント関数
    public void AttackOver()
    {
        comboCounter_++;

        //三段攻撃終わったらCounter数をリセット 攻撃状態終わる
        if (comboCounter_ > 2)
        {
            comboCounter_ = 0;
            isAttacking_ = false;
            isGround_ = true;
        }
    }

    #endregion

    #region NeroSaika function space
    // ----------------------------------------- Renne function space -----------------------------------------
    // TODO: NeroSaika's function workspace
    private void DashingState()
    {

    }

    #endregion
}
