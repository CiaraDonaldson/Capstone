using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sneak : MonoBehaviour
{
    public float radius = 2f;
    public float digDepth = 1f;
    public LayerMask digLayer;
    public LayerMask groundLayer;
    public CircleCollider2D cCollider;
    public CapsuleCollider2D capCollider;
    public Animator anim;
    public Sprite thisSprite;
    public Rigidbody2D rb;
    public SpriteRenderer rend;
    public bool inAir = false;
    public float sneakForce = 5f;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        cCollider = GetComponent<CircleCollider2D>();
        capCollider = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 raycastOrigin = transform.position;
        Vector3 raycastDirection = transform.forward;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, digDepth, digLayer);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, Vector2.down, digDepth, groundLayer);
        Debug.DrawRay(raycastOrigin, raycastDirection * 2, Color.green);

        if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow))
            {
            rb.AddForce(Vector3.left * sneakForce, ForceMode2D.Impulse);
            anim.Play("Sneak");
        }
        else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow))
            {
            rb.AddForce(Vector3.right * sneakForce, ForceMode2D.Impulse);
            anim.Play("Sneak");
        }

        if (!hit && !hit2 && !Input.anyKey)
        {
            inAir = true;
            // anim.Play("Fall");
        }
        else if (hit | hit2)
        {
            inAir = false;


        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            float velocityMagnitude = rb.velocity.magnitude;

            if (!hit)
            {
                cCollider.enabled = false;
                capCollider.enabled = true;
            }
            if (!hit & velocityMagnitude < 1)
            {
                anim.enabled = false;
                rend.sprite = thisSprite;
            }
            else
            {
                anim.enabled = true;
            }

            if (rb.velocity.x != 0)
            {

                anim.Play("Sneak");
            }
          
        }
        else
        {
            anim.enabled = true;
            cCollider.enabled = true;
            capCollider.enabled = false;
        }
    }
}
