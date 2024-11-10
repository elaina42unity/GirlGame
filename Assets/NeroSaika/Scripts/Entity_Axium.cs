using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_Axium : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator anim;
    protected int facingDir = 1;
    protected bool facingRight = true;

    [Header("Collision info")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;
    protected bool isGrounded;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        anim= GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        CollisionChecks();
    }

    //Check isGrouned or not
    protected virtual void CollisionChecks()
    {
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    }

    //Make facing direction go to the other hand
    protected virtual void flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    //Make a line to get isGrounded or not
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
    }
}
