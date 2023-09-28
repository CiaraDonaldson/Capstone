using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carry : MonoBehaviour
{
    public KeyCode carryKey = KeyCode.E;  
    public float carryForce = 20f;           
    public float flyForce = 25f;           
    public Rigidbody2D playerToCarry;
    public bool isTouching = false;
    public GameObject Fox;
    public ParticleSystem dust;
    public GameObject fDust;

    private FixedJoint2D joint;
    private bool isCarrying = false;
    private Rigidbody2D rb;
    void Start()
    {
        fDust = GameObject.Find("FDust");
        rb = GetComponent<Rigidbody2D>();
        Fox = GameObject.Find("Fox");
        // A FixedJoint2D component to connect the two players.
        joint = playerToCarry.gameObject.AddComponent<FixedJoint2D>();
        joint.connectedBody = GetComponent<Rigidbody2D>();
        joint.enabled = false; // Initially disabled.
    }

    void Update()
    {
        if (Input.GetKeyDown(carryKey))
        {
            ToggleCarry();
            
        }

        if (isCarrying)
        {
            //rb.gravityScale = 0;
            playerToCarry.gravityScale = 0;

            // Apply carry force to move the carried player.
            Vector2 carryDirection = (playerToCarry.transform.position - transform.position).normalized;
            rb.AddForce(carryDirection * carryForce);

            // Apply upward force for flying.
            if (Input.GetKeyDown(KeyCode.W))
            {
                CreateDust();
                rb.AddForce(Vector3.up * flyForce, ForceMode2D.Impulse);
            }


        }
        else 
        {
            playerToCarry.gravityScale = 2;
           

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fox"))
        {
            isTouching = true;
        }
        else 
        {
            isTouching = false;
        }
    }


        void ToggleCarry()
    {
        
        isCarrying = !isCarrying;

        if (isCarrying)
        {
            joint.enabled = true;
            Fox.GetComponent<Taunt>().enabled = false;
            fDust.SetActive(false);
            this.GetComponent<Scan>().enabled = false;
            this.GetComponent<Fly>().enabled = false;
        }
        else
        {
            joint.enabled = false;
            Fox.GetComponent<Taunt>().enabled = true;
            fDust.SetActive(true);
            this.GetComponent<Scan>().enabled = true;
            this.GetComponent<Fly>().enabled = true;
        }
    }
    void CreateDust()
    {
        dust.Play();
    }
}