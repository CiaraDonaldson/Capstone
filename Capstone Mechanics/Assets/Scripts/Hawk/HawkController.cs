using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HawkController : MonoBehaviour
{

    public Rigidbody2D PlayerRb;
    public float jumpForce = 15f;
    public float walkForce = 5f;

    private int points;
    //private GameController gameControllerReference;

    void Start()
    {
        PlayerRb = GetComponent<Rigidbody2D>();
        // gameControllerReference = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }
        // Update is called once per frame
        private void Update()
    {
      
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



   
}
