using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveForce = 15f;
    public LayerMask groundLayer;
    //public LayerMask digLayer;
    public bool isGrounded = false;
    // public bool canJump = false;

    //private GameController gameControllerReference;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // gameControllerReference = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(Vector3.left * moveForce, ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(Vector3.right * moveForce, ForceMode2D.Impulse);
        }
        //Checks for ground
        //isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, groundLayer);

        // Allow jumping if grounded or on the "dig" layer and the jump button is pressed.
        //if (isGrounded || Physics2D.Raycast(transform.position, Vector2.down, 0.1f, digLayer))
        // {
        //     canJump = true;
        // }
        // else
        // {
        //     canJump = false;
        // }

        // Perform the jump when the jump button is pressed and allowed.
        //if (Input.GetKeyDown(KeyCode.UpArrow))
        //{
       //    rb.velocity = new Vector2(rb.velocity.x, moveForce);
       // }
    }
    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }*/
}

