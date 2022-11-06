using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{ 
    bool isGrounded;
    public float jumpForce = 10f;
    public float speed = 10f;
    private Rigidbody2D rb;

    //Flip sprites for left and right movement
    bool facingRight;
    public SpriteRenderer spriteRenderer, spriteRenderer2;

    private Animator animator;

    bool isAttacking;
    bool isMoving;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Horizontal movement
        float horizontal = Input.GetAxis("Horizontal");
        if (horizontal != 0)
        {
            isMoving = true;
            animator.SetBool("isWalking", isMoving);
            rb.velocity = new Vector3(horizontal * speed, rb.velocity.y, 0);
            if (horizontal > 0 && !facingRight)
            {
                Flip();
            }
            else if (horizontal < 0 && facingRight)
            {
                Flip();
            }
        }
        else
        {
            isMoving = false;
            animator.SetBool("isWalking", isMoving);
        }

        //Make player jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
            isGrounded = false;
        }

        //Attack
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            isAttacking = true;
            animator.SetBool("isAttacking", isAttacking);
            spriteRenderer2.enabled = true;
        }
        else
        {
            isAttacking = false;
            animator.SetBool("isAttacking", isAttacking);
            spriteRenderer2.enabled = false;
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        spriteRenderer.flipX = !spriteRenderer.flipX;
        spriteRenderer2.flipX = !spriteRenderer2.flipX;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            isGrounded = true;
        }
    }
}
