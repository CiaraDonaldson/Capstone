using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    public Rigidbody2D PlayerRb;
    public float flyForce = 15f;
    // Start is called before the first frame update
    void Start()
    {
        PlayerRb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            JumpUp();
        }

    }
    void JumpUp()
    {
        PlayerRb.AddForce(Vector3.up * flyForce, ForceMode2D.Impulse);
    }
}
