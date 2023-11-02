using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    public Rigidbody2D PlayerRb;
    public float flyForce = 15f;
    public ParticleSystem dust;

    public CapsuleCollider2D capsule;

    // Start is called before the first frame update
    void Start()
    {
        PlayerRb = GetComponent<Rigidbody2D>();
        capsule = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        /* if (PlayerRb.velocity.y > 0)
         {
             capsule.enabled = false;
         }
         else 
         {
             capsule.enabled = true;

         }*/
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.0f))
        {
            // Check if the ground is tagged as "Ground". Adjust the tag accordingly.
            if (hit.collider.CompareTag("Ground"))
            {
                // Player is on the ground, enable the collider.
                capsule.enabled = true;
            }
            else
            {
                // Player is in the air, disable the collider.
                capsule.enabled = false;
            }

        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            CreateDust();
            JumpUp();
        }

    }
    void JumpUp()
    {
        PlayerRb.AddForce(Vector3.up * flyForce, ForceMode2D.Impulse);
    }

    /* private void OnTriggerEnter2D(Collider2D other)
      {
          if (other.CompareTag("Ground"))
          {
              capsule.enabled = true;
          }
          else
          {
              capsule.enabled = false;

          }
      }*/

    void CreateDust()
    {
        dust.Play();
    }
}
