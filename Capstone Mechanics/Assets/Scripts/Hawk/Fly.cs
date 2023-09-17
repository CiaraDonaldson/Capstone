using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    public Rigidbody2D PlayerRb;
    public float flyForce = 15f;


    public CapsuleCollider2D capsule;
    public BoxCollider2D box;

    // Start is called before the first frame update
    void Start()
    {
        PlayerRb = GetComponent<Rigidbody2D>();
        capsule = GetComponent<CapsuleCollider2D>();
        box = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerRb.velocity.y > 0)
        {
            capsule.enabled = false;
        }
        else 
        {
            capsule.enabled = true;

        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            JumpUp();
        }

    }
    void JumpUp()
    {
        PlayerRb.AddForce(Vector3.up * flyForce, ForceMode2D.Impulse);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            capsule.enabled = true;
        }
        else
        {
            capsule.enabled = false;

        }
    }
   
}
