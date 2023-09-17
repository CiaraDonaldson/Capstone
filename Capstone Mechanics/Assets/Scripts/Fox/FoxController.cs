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
        
    }
  
}

