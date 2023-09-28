using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HawkController : MonoBehaviour
{

    public Rigidbody2D PlayerRb;
    public float jumpForce = 15f;
    public float walkForce = 5f;

    private int points;
    private Transform characterTransform;
    private bool isFacingRight = true;

    public Collider2D cCircle;
    public Collider2D cCapsule;
    //private GameController gameControllerReference;

    void Start()
    {
        characterTransform = transform;

        PlayerRb = GetComponent<Rigidbody2D>();
        // gameControllerReference = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            FlipCharacter(false);  // Flip to face left.
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            FlipCharacter(true);   // Flip to face right.
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            JumpDown();
        }
        if (Input.GetKey(KeyCode.A))
        {
            JumpLeft();
        }
        if (Input.GetKey(KeyCode.D))
        {
            JumpRight();
        }
    }
    void JumpDown()
    {
        PlayerRb.AddForce(Vector3.down * walkForce, ForceMode2D.Impulse);
    }
    void JumpLeft()
    {
        PlayerRb.AddForce(Vector3.left * walkForce, ForceMode2D.Impulse);
    }
    void JumpRight()
    {
        PlayerRb.AddForce(Vector3.right * walkForce, ForceMode2D.Impulse);
    }

    void FlipCharacter(bool faceRight)
    {
        // Flip the character's scale based on the direction.
        Vector3 newScale = characterTransform.localScale;
        newScale.x = faceRight ? 1 : -1;
        characterTransform.localScale = newScale;

        isFacingRight = faceRight;
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            cCircle.enabled = false;
        }
        else
        {
            cCircle.enabled = true;
        }
    }

}
