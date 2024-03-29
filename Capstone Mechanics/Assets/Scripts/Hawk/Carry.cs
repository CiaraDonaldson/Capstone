using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carry : MonoBehaviour
{
    public float carryForce = 20f;           
    public float flyForce = 25f;           
    public Rigidbody2D playerToCarry;
    //public bool isTouching = false;
    public GameObject Fox;
    public ParticleSystem dust;
    public GameObject fDust;

    private FixedJoint2D joint;
    public bool isCarrying = false;
    private Rigidbody2D rb;
    private Animator anim;
    private bool isFacingRight = true;

    void Start()
    {
        fDust = GameObject.Find("FDust");
        rb = GetComponent<Rigidbody2D>();
        Fox = GameObject.Find("Fox");
        anim = GetComponent<Animator>();

        // A FixedJoint2D component to connect the two players.
        joint = playerToCarry.gameObject.AddComponent<FixedJoint2D>();
        joint.connectedBody = GetComponent<Rigidbody2D>();
        joint.enabled = false; // Initially disabled.
        
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            Transform tranFox = Fox.GetComponent<Transform>();
            float distance = GetDistance(tranFox.position, this.transform.position);
            if (distance < 2.3)
            {               
                ToggleCarry();
            }           
            
        }
        
        if (isCarrying)
        {
            //rb.gravityScale = 0;
            playerToCarry.gravityScale = 0;

            // Apply carry force to move the carried player.
            //Vector2 carryDirection = (playerToCarry.transform.position - transform.position).normalized;
            //rb.AddForce(carryDirection * carryForce);

            if (Input.GetKeyDown(KeyCode.A))
            {
                FlipCharacter(false);  // Flip to face left.
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                FlipCharacter(true);   // Flip to face right.
            }

            // Apply upward force for flying.
            if (Input.GetKeyDown(KeyCode.W))
            {
                CreateDust();
                rb.AddForce(Vector3.up * flyForce, ForceMode2D.Impulse);
            }

            Transform tranFox = Fox.GetComponent<Transform>();
            float distance = GetDistance(tranFox.position, this.transform.position);
            if (distance > 2.3)
            {
                ToggleCarry();
            }

        }
        else 
        {
            playerToCarry.gravityScale = 2;
        }
    }

    float GetDistance(Vector3 fPos, Vector3 hPos)
    {
        return Vector3.Distance(fPos, hPos);
    }
   

    void ToggleCarry()
    {        
        isCarrying = !isCarrying;

        if (isCarrying)
        {
            this.transform.Translate(Vector3.up * 1f);

            joint.enabled = true;
            Fox.GetComponent<Taunt>().enabled = false;
            Fox.GetComponent<Dig>().enabled = false;
            Fox.GetComponent<Sneak>().enabled = false;
            fDust.SetActive(false);
            this.GetComponent<Scan>().enabled = false;
            this.GetComponent<Fly>().enabled = false;

            anim.Play("Carry");
            Fox.GetComponent<Animator>().Play("Carried");
        }
        else
        {
            Fox.transform.Translate(Vector3.up * 1f);
            joint.enabled = false;
            Fox.GetComponent<Taunt>().enabled = true;
            Fox.GetComponent<Dig>().enabled = true;
            Fox.GetComponent<Sneak>().enabled = true;
            fDust.SetActive(true);
            this.GetComponent<Scan>().enabled = true;
            this.GetComponent<Fly>().enabled = true;
           
        }
    }

    void FlipCharacter(bool faceRight)
    {
        // Flip the character's scale based on the direction.
        Vector3 newScale = Fox.transform.localScale;
        newScale.x = faceRight ? 1 : -1;
        Fox.transform.localScale = newScale;

        isFacingRight = faceRight;
    }

    void CreateDust()
    {
        dust.Play();
    }
}