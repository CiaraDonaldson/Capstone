using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HTalk : MonoBehaviour
{
    public GameObject popUpBox;
    public Animator animator;
    public TMP_Text popUpText;
    public string popUptext;
  
    private GameObject Hawk;
    private GameObject Fox;
    private Vector3 initialScale;
    private Quaternion initialRotation;

    public int scanreact = 0;
    // Start is called before the first frame update
    void Start()
    {
        initialScale = transform.localScale;
        initialRotation = transform.rotation;
        Hawk = GameObject.Find("Hawk");
        Fox = GameObject.Find("Fox");
    }

    // Update is called once per frame
    void Update()
    {
        if (!Fox.GetComponent<Sneak>().inAir)
        {
            StartCoroutine(canScanNeg());
        }
        if (Fox.GetComponent<Sneak>().inAir)
        {
            PopDown();
        }
        //&& this.GetComponent<Scan>().wKeyPressStartTime > 3
        if (scanreact == 0) { 
       // switch (scanreact)
        //{
           // case 0:
          
            //break;
        }
        else if (scanreact == 1)
        {
            //   case 1:
            if (!Fox.GetComponent<Sneak>().inAir)
            {
                StartCoroutine(canScanNeu());
            }
            if (Fox.GetComponent<Sneak>().inAir)
            {
                PopDown();
            }
        }
        //       break;
        else if (scanreact == 2)
        {
            //   case 2:
            if (!Fox.GetComponent<Sneak>().inAir)
            {
                StartCoroutine(canScanPos());
            }
            if (Fox.GetComponent<Sneak>().inAir)
            {
                PopDown();
            }
        }
          //      break;
         //   default:
          //      Debug.LogError("Invalid reaction value");
         //       break;
       // }

    }
   
    IEnumerator canScanNeg()
        {
            PopUp("Stupid Fox! Can you touch the ground!?");
            yield return new WaitForSeconds(5f);
            //PopDown();
        }
    IEnumerator canScanNeu()
    {
        PopUp("Hey Fox, can you reach the ground?");
        yield return new WaitForSeconds(5f);
        //PopDown();
    }
    IEnumerator canScanPos()
    {
        PopUp("Hey Fox, can you please land?");
        yield return new WaitForSeconds(5f);
        //PopDown();
    }
    public void PopUp(string text)
    {
        popUpBox.SetActive(true);
        popUpText.text = text;
        animator.Play("pop");
    }
    public void PopDown()
    {
        animator.Play("close");
        //popUpBox.SetActive(false);
    }
}

