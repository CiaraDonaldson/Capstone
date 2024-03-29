using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Sneak : MonoBehaviour
{
    public float radius = 2f;
    public float digDepth = 2f;
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
    public GameObject Hawk;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        cCollider = GetComponent<CircleCollider2D>();
        capCollider = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        Hawk = GameObject.Find("Hawk");

    }

    // Update is called once per frame
    void Update()
    {
        if (!Hawk.GetComponent<Scan>().isElevating)
        {
            Vector3 raycastOrigin = transform.position;
        Vector3 raycastDirection = transform.forward;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, digDepth, digLayer);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, Vector2.down, digDepth, groundLayer);
        Vector3 down = transform.TransformDirection(Vector3.down) * digDepth;
        Debug.DrawRay(transform.position, down, Color.red);

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


        if (hit.collider != null || hit2.collider != null)
        {
            inAir = false;

        }
        else
        {
            inAir = true;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            float velocityMagnitude = rb.velocity.magnitude;



            if (hit.collider != null)
            {
                cCollider.enabled = false;
                capCollider.enabled = true;
            }
            if (hit.collider != null & velocityMagnitude < 1)
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
}
