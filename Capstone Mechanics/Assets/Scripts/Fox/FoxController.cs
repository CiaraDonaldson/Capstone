using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxController : MonoBehaviour
{
    public ParticleSystem dust;
    public Rigidbody2D rb;
    public float moveForce = 15f;
    public float sneakForce = 5f;
    public LayerMask groundLayer;
    public bool isGrounded = false;
    public Animator anim;
    public SpriteRenderer rend;
    

    private Transform characterTransform;
    private bool isFacingRight = true;
    //private GameController gameControllerReference;

    void Start()
    {
        characterTransform = transform;
        //dust = GetComponent<ParticleSystem>();
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        //GameObject particleSystemObject = GameObject.Find("FDust");
        //ParticleSystem particleSystem = particleSystemObject.GetComponent<ParticleSystem>();


        // Try to get the ParticleSystem component


        // gameControllerReference = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }
    // Update is called once per frame
    void Update()
    {
       // GameObject particleSystemObject = GameObject.Find("FDust");
       // ParticleSystem particleSystem = particleSystemObject.GetComponent<ParticleSystem>();

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            FlipCharacter(false);  // Flip to face left.
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            FlipCharacter(true);   // Flip to face right.
        }


        if (!Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(Vector3.left * moveForce, ForceMode2D.Impulse);
            anim.Play("Run");
        }
        else if (!Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(Vector3.right * moveForce, ForceMode2D.Impulse);
            anim.Play("Run");
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(Vector3.left * sneakForce, ForceMode2D.Impulse);
            anim.Play("Sneak");
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(Vector3.right * sneakForce, ForceMode2D.Impulse);
            anim.Play("Sneak");
        }
        else 
        {
            GetComponent<ParticleSystem>().Stop();
            anim.Play("Idle");
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

