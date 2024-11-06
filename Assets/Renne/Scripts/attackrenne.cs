using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackrenne : MonoBehaviour
{
    private bool isAttacking;
    private int comboCounter;

    //comboを維持する時間
    private float comboTime = 1.0f;
    //comboを維持する時間の減算
    private float comboTimeWindow;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetInteger("comboCounter", comboCounter);
        anim.SetBool("isAttacking", isAttacking);
       
        comboTimeWindow -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartAttackEvent();
        }

    }

    private void StartAttackEvent()
    {
        //維持する時間に経ったらcombo数をリセット
        if (comboTimeWindow < 0)
        {
            comboCounter = 0;
        }
        isAttacking = true;

        comboTimeWindow = comboTime;
    }

    //アニメーターイベント関数
    public void AttackOver()
    {
        isAttacking = false;

        comboCounter++;

        //三段攻撃終わったらCounter数をリセット
        if (comboCounter > 2)
        {
            comboCounter = 0;
        }
    }
}
