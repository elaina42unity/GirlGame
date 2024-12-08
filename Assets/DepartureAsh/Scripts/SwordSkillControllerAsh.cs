using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSkillControllerAsh : MonoBehaviour
{
    private Animator anim_;
    private Rigidbody2D rb_;
    private CircleCollider2D cd_;
    private PlayerAsh player_;

    private void Start()
    {
    
    }
    private void Awake()
    {
        anim_ = GetComponentInChildren<Animator>();
        rb_ = GetComponent<Rigidbody2D>();
        cd_ = GetComponent<CircleCollider2D>();
    }
    public void SetupSword(Vector2 dir,float gravityScale)
    {
        rb_.velocity = dir;
        rb_.gravityScale = gravityScale;
    }
}
