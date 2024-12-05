using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBallSkillControllerAX : MonoBehaviour
{
    [SerializeField] private float returnSpeed;
    private Animator anim;
    private Rigidbody2D rb;
    private CircleCollider2D cd;
    private PlayerAX player;    
    private bool canRotate = true;
    private bool isReturning;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        cd = GetComponent<CircleCollider2D>(); 

    }
    private void Start()
    {
    }

    public void SetupWaterBall(Vector2 _dir,float _gravityScale,PlayerAX _palyer)
    {
        player = _palyer;
        rb.velocity = _dir;
        rb.gravityScale = _gravityScale;

        anim.SetBool("Attack",true);
    }

    public void ReturnWaterBall()
    {
        rb.isKinematic = false;
        transform.parent = null;
        isReturning = true;
    }

    private void Update()
    {
        if (canRotate)
        transform.right = rb.velocity;

        if (isReturning)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, returnSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, player.transform.position) < 2)
                player.ClearTheWaterBall();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        anim.SetBool("Attack",false);

        canRotate = false;
        cd.enabled = false;

        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        transform.parent = collision.transform;
    }
}
