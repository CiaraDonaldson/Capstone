using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxController : MonoBehaviour
{
    public ParticleSystem dust;
    public Rigidbody2D rb;
    public float moveForce = 15f;
    public LayerMask groundLayer;
    //public LayerMask digLayer;
    public bool isGrounded = false;
    // public bool canJump = false;

    private Transform characterTransform;
    private bool isFacingRight = true;
    //private GameController gameControllerReference;

    void Start()
    {
        characterTransform = transform;
        //dust = GetComponent<ParticleSystem>();
        rb = GetComponent<Rigidbody2D>();
        // gameControllerReference = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            FlipCharacter(false);  // Flip to face left.
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            FlipCharacter(true);   // Flip to face right.
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(Vector3.left * moveForce, ForceMode2D.Impulse);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(Vector3.right * moveForce, ForceMode2D.Impulse);
        }
        
    }
    void FlipCharacter(bool faceRight)
    {
        CreateDust();
        // Flip the character's scale based on the direction.
        Vector3 newScale = characterTransform.localScale;
        newScale.x = faceRight ? 1 : -1;
        characterTransform.localScale = newScale;

        isFacingRight = faceRight;
    }

    void CreateDust()
    {
        dust.Play();
    }
}

