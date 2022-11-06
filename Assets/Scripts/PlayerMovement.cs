using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{ 
    bool isGrounded;
    public float jumpForce = 10f;
    public float speed = 10f;

    //Flip sprites for left and right movement
    bool facingRight;
    public SpriteRenderer spriteRenderer;

    private Animator animator;

    bool isAttacking;
    bool isMoving;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Move player to the right
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(speed, 0, 0);
            isMoving = true;
            facingRight = true;
        }
        else
        {
            isMoving = false;
        }
        
        //Move player to the left
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-speed, 0, 0);
            isMoving = true;
            facingRight = false;
            
        }
        else
        {
            isMoving = false;
        }

        //Attack
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }

        //Make player jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            isGrounded = true;
        }
    }

    private void FixedUpdate()
    {
        //Flip sprites
        if (facingRight)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

        //Start walking animation
        if (isMoving)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        //Start attack animation
        if (isAttacking)
        {
            animator.SetBool("isAttacking", true);
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }
    }
}
