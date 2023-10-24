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

            StartCoroutine(PullHawkUpAndEnableCollider());
        }
        if (isHolding)
        {
            // Calculate the desired position above the holder.
            Vector3 holdPosition = transform.position + Vector3.up * holdingHeight;

            // Move the held player to the desired position.
            playerToHold.position = holdPosition;
        }
       
    }

    void FixedUpdate()
    {
        if (isHolding)
        {
            Hawk.GetComponent<CapsuleCollider2D>().enabled = false;
            Hawk.GetComponent<Carry>().enabled = false;
            Hawk.GetComponent<Fly>().enabled = false;
            Hawk.GetComponent<Scan>().enabled = false;

        }
        else 
        {
            //Hawk.GetComponent<CapsuleCollider2D>().enabled = true;
            Hawk.GetComponent<Carry>().enabled = true;
            Hawk.GetComponent<Fly>().enabled = true;
            Hawk.GetComponent<Scan>().enabled = true;

        }
    }
    IEnumerator PullHawkUpAndEnableCollider()
    {
        // Check if the hawk is one unit below the fox.
        if (Mathf.Abs(Hawk.transform.position.y - this.transform.position.y) <= 1.0f)
        {
            // Pull the hawk up by 3 units.
            Hawk.transform.Translate(Vector3.up * 1.5f);

            // Wait for one second.
            yield return new WaitForSeconds(.1f);

            // Enable the hawk's collider.
            Hawk.GetComponent<CapsuleCollider2D>().enabled = true;
        }
        else
        {
            Hawk.GetComponent<CapsuleCollider2D>().enabled = true;
        }
    }
}
