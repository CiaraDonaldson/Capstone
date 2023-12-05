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
    public float digDepth = 1f;
    public LayerMask digLayer;
    public GameObject Hawk;


    private Transform characterTransform;
    private bool isFacingRight = true;
    //private GameController gameControllerReference;

    void Start()
    {
        characterTransform = transform;
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        GameObject particleSystemObject = GameObject.Find("FDust");
        dust = particleSystemObject.GetComponent<ParticleSystem>();
        Hawk = GameObject.Find("Hawk");

    }
    // Update is called once per frame
    void Update()
    {
        GameObject particleSystemObject = GameObject.Find("FDust");
        dust = particleSystemObject.GetComponent<ParticleSystem>();
        if (dust == null)
        {
            Debug.Log("Cant Find The Dust!");
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            FlipCharacter(false);  // Flip to face left.
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            FlipCharacter(true);   // Flip to face right.
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, digDepth, digLayer);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, Vector2.down, digDepth, groundLayer);

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
        else if (!Input.anyKey & hit | hit2)
        {
            dust.Stop();
            //anim.Play("Fall");
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
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Tilemap" | collision.gameObject.name == "Top Dig" | collision.gameObject.name == "Bottom Dig" && !Input.anyKey)
        {
            if (!Hawk.GetComponent<Carry>().isCarrying)
            {
                anim.Play("Idle");
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!Input.anyKey && collision.gameObject.name == "Tilemap" | collision.gameObject.name == "Top Dig" | collision.gameObject.name == "Bottom Dig")
        {
            if (!Hawk.GetComponent<Carry>().isCarrying) 
            {
                anim.Play("Fall");
            }
           
        }
    }
}

