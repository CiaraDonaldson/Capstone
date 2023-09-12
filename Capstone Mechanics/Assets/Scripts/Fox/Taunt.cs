using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taunt : MonoBehaviour
{ 
    public Transform playerToHold;          
    public float holdingHeight = 1.5f;    
    public Transform chain;
    public  GameObject Hawk;


    private bool isHolding = false;
    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = playerToHold.position;
        //Hawk.GetComponent<Carry>();
        Hawk = GameObject.Find("Hawk");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            isHolding = true;
           
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            isHolding = false;
            playerToHold.position = chain.position;

        }
    }

    void FixedUpdate()
    {
        if (isHolding)
        {
            // Calculate the desired position above the holder.
            Vector3 holdPosition = transform.position + Vector3.up * holdingHeight;

            // Move the held player to the desired position.
            playerToHold.position = holdPosition;

            Hawk.GetComponent<CapsuleCollider2D>().enabled = false;
            Hawk.GetComponent<Carry>().enabled = false;
            Hawk.GetComponent<Fly>().enabled = false;
            Hawk.GetComponent<Scan>().enabled = false;

        }
        else 
        {
            Hawk.GetComponent<CapsuleCollider2D>().enabled = true;
            Hawk.GetComponent<Carry>().enabled = true;
            Hawk.GetComponent<Fly>().enabled = true;
            Hawk.GetComponent<Scan>().enabled = true;

        }
    }
}
