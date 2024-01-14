using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class HTalk : MonoBehaviour
{
    public LayerMask digLayer;
    public LayerMask groundLayer;

    public GameObject popUpBox;
    public Animator animator;
    public TMP_Text popUpText;
    public string popUptext;

    public GameObject fpopUpBox;
    public Animator fanimator;
    public TMP_Text fpopUpText;
    public string fpopUptext;

    private GameObject Hawk;
    private GameObject Fox;
    private Vector3 initialScale;
    private Quaternion initialRotation;

    public int scanreact = 0;
    private float wKeyHoldTime = 0f;
    private bool hasCounted = false;
    private int keyHoldCounter = 0;

    [SerializeField] private int digCount = 0;
    private bool isCoroutineRunning = false;
    private bool hasLvl2AirRun = false;
    [SerializeField] private int Count = 0;

    private bool isFacingRight = true;
    private bool Lvl8Play = false;

    // Start is called before the first frame update
    void Start()
    {
        initialScale = transform.localScale;
        initialRotation = transform.rotation;
        Hawk = GameObject.Find("Hawk");
        Fox = GameObject.Find("Fox");
        // holdnum = sayOnceDig;

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
     /*   if (sceneName == "Lvl4")
        {
            digCount = 0;
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        //LEVEL 2
       Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "Lvl2")
        {
            RaycastHit2D hit = Physics2D.Raycast(Fox.transform.position, Vector2.down, 15, digLayer);
            RaycastHit2D hit2 = Physics2D.Raycast(transform.position, Vector2.down, 15, groundLayer);
            Vector3 down = transform.TransformDirection(Vector3.down) * 15;
            Debug.DrawRay(transform.position, down, Color.red);

            if (hit && Input.GetKeyDown(KeyCode.DownArrow))
            { 
                digCount++; 
            }

            if (!isCoroutineRunning && digCount == 2)
            {
                StartCoroutine(Lvl2Dig());
            }


            if (hit.collider == null && hit2.collider == null && this.GetComponent<Carry>().isCarrying && !isCoroutineRunning && !hasLvl2AirRun)
            {
                StartCoroutine(Lvl2Air());

            }
        }

        //LEVEL 4
        if (sceneName == "Lvl4")
        {
            if (Input.GetKey(KeyCode.W))
            {
                wKeyHoldTime += Time.deltaTime;
                if (wKeyHoldTime >= 7f && !hasCounted)
                {
                    digCount++;
                    //wKeyHoldTime = 0f;
                    hasCounted = true;
                }
            }
            else
            {
                wKeyHoldTime = 0f; 
                hasCounted = false;
            }
            
            if (!isCoroutineRunning && digCount == 2)
            {
                StartCoroutine(Lvl4Scan());
            }
            RaycastHit2D hit = Physics2D.Raycast(Fox.transform.position, Vector2.down, 2, groundLayer);
            //RaycastHit2D hit2 = Physics2D.Raycast(transform.position, Vector2.down, 15, groundLayer);
            Vector3 down = transform.TransformDirection(Vector3.down) * 15;
            Debug.DrawRay(transform.position, down, Color.red);

            if (hit && Input.GetKeyDown(KeyCode.DownArrow))
            {
                Count++;
            }

            if (!isCoroutineRunning && Count == 2)
            {
                StartCoroutine(Lvl4Sneak());
            }
            
        }

        //LEVEL 6
        if (sceneName == "Lvl6")
        {
            if (Input.GetKeyDown(KeyCode.S) && !this.GetComponent<Carry>().isCarrying)
            {
                Count++;
            }
            if (Count == 3)
            {
                Count++;
                StartCoroutine("Lvl6Carry");
            }
            if (Input.GetKeyDown(KeyCode.S) && this.GetComponent<Carry>().isCarrying)
            {
                digCount++;
            }
            if (digCount == 1)
            {
                digCount++;
                StartCoroutine("Lvl6Drop");
            }
        }

        //LEVEL 8
        if (sceneName == "Lvl8")
        {
            if (!isCoroutineRunning && !Lvl8Play)
            {
                StartCoroutine("Lvl8Pass");
            }
        }

        //ALL LEVELS
        if (Input.GetKeyDown(KeyCode.A))
        {
            FlipHBox(false);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            FlipHBox(true);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            FlipFBox(false);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            FlipFBox(true);
        }
        
        if (Input.GetKey(KeyCode.W))
        {
            wKeyHoldTime += Time.deltaTime;
        }
        else
        {
            wKeyHoldTime = 0f; // Reset the timer if W key is not held
        }
        switch (scanreact)
        {
            case 0:
                if (Fox.GetComponent<Sneak>().inAir && wKeyHoldTime >= 3f)
                {
                    StartCoroutine(canScanNeg());
                    }
                    if (!Fox.GetComponent<Sneak>().inAir && !isCoroutineRunning)
                    {
                    StopCoroutine(canScanNeg());
                    PopDown();
                    }
                
      break;
        
      //  else if (scanreact == 1)
      //  {
            case 1:

                if (Fox.GetComponent<Sneak>().inAir && wKeyHoldTime >= 3f)
                {
                    StartCoroutine(canScanNeu());
                    }
                    if (!Fox.GetComponent<Sneak>().inAir && !isCoroutineRunning)
                    {
                    StopCoroutine(canScanNeu());
                    PopDown();
                    }
                
               break;
       // else if (scanreact == 2)
        //{
              case 2:
               
                if (Fox.GetComponent<Sneak>().inAir && wKeyHoldTime >= 3f)
                    {
                        StartCoroutine(canScanPos());
                    }
                    if (!Fox.GetComponent<Sneak>().inAir && !isCoroutineRunning)
                    {
                    StopCoroutine(canScanPos());
                    PopDown();
                    }
                
       // }
               break;
           default:
                Debug.LogError("Invalid reaction value");
                break;
        }

    }

    IEnumerator Lvl2Air()
    {
        isCoroutineRunning = true;
        fPopUp("Stop flying so high, before you kill us both bird brain");
        yield return new WaitForSeconds(5f);

        PopUp("If I could, I’d drop you from the highest point");
        yield return new WaitForSeconds(5f);
       // PopDown();
       
        // fPopDown();
        StartCoroutine(PlayCloseAnimation());

        isCoroutineRunning = false;
        hasLvl2AirRun = true;
    }
    IEnumerator Lvl2Dig()
    {
        isCoroutineRunning = true;

        PopUp("Can you dig any faster mutt!?");

        yield return new WaitForSeconds(5f);

        fPopUp("Fly into a tree, overgrown chicken");
        yield return new WaitForSeconds(5f);

        digCount++;

        // Play the close animation
        StartCoroutine(PlayCloseAnimation());

        isCoroutineRunning = false;
    }
    IEnumerator Lvl4Scan()
    {
        isCoroutineRunning = true;
        fPopUp("You had the ability to see all the orbs this entire time and just now using it.");
        yield return new WaitForSeconds(5f);

        PopUp("...Short answer yes, long answer no");
        yield return new WaitForSeconds(5f);

        fPopUp("Your a moron");
        yield return new WaitForSeconds(5f);

        digCount++;
        // Play the close animation
        StartCoroutine(PlayCloseAnimation());
       
        isCoroutineRunning = false;
    }
    IEnumerator Lvl4Sneak()
    {
        isCoroutineRunning = true;

        PopUp(" You’re a feline");
        yield return new WaitForSeconds(5f);

        fPopUp("What !?! No I’m not");
        yield return new WaitForSeconds(3f);

        PopUp("Might need to talk to your mother then.");
        yield return new WaitForSeconds(5f);

       Count++;
        // Play the close animation
        StartCoroutine(PlayCloseAnimation());

        isCoroutineRunning = false;
    }
    IEnumerator Lvl6Carry()
    {
        isCoroutineRunning = true;

        PopUp("If it’s any consolation, I hate this too..");

        yield return new WaitForSeconds(5f);

        fPopUp("I hate you.");
        yield return new WaitForSeconds(5f);

        

        // Play the close animation
        StartCoroutine(PlayCloseAnimation());

        isCoroutineRunning = false;
    }
    IEnumerator Lvl6Drop()
    {
        isCoroutineRunning = true;

        fPopUp("OW!?!");
        yield return new WaitForSeconds(3f);

        PopUp("Oops..");
        yield return new WaitForSeconds(3f);

        fPopUp("Yknow this hurts you too right??");
        yield return new WaitForSeconds(5f);

        PopUp("Eh, Worth it..");
        yield return new WaitForSeconds(5f);

        //digCount++;

        // Play the close animation
        StartCoroutine(PlayCloseAnimation());

        isCoroutineRunning = false;
    }
    IEnumerator Lvl8Pass()
    {
        isCoroutineRunning = true;
        yield return new WaitForSeconds(10f);

        fPopUp("Are you allergic to the ground?");
        yield return new WaitForSeconds(3f);

        PopUp("What are you on about?");
        yield return new WaitForSeconds(3f);

        fPopUp("You're just always flying, not once have you landed");
        yield return new WaitForSeconds(5f);

        fPopUp("Do your wings not hurt??");
        yield return new WaitForSeconds(4f);

        PopUp("no...?");
        yield return new WaitForSeconds(3f);

        //digCount++;
        Lvl8Play = true;
        // Play the close animation
        StartCoroutine(PlayCloseAnimation());

        isCoroutineRunning = false;
    }
    IEnumerator PlayCloseAnimation()
    {
        yield return new WaitForSeconds(2f); // Adjust the delay if needed
        PopDown();
        fPopDown();
    }
    IEnumerator canScanNeg()
        {
        yield return new WaitForSeconds(3f);
        PopUp("Stupid Fox! Can you touch the ground!?");
            //yield return new WaitForSeconds(5f);
            //PopDown();
        }
    IEnumerator canScanNeu()
    {
        yield return new WaitForSeconds(3f);
        PopUp("Hey Fox, can you reach the ground?");
        //yield return new WaitForSeconds(5f);
        //PopDown();
    }
    IEnumerator canScanPos()
    {
        yield return new WaitForSeconds(3f);
        PopUp("Hey Fox, can you please land?");
        //yield return new WaitForSeconds(5f);
        //PopDown();
    }
    public void PopUp(string text)
    {
        popUpBox.SetActive(true);
        popUpText.text = text;
        animator.Play("pop");
    }
    public void fPopUp(string text)
    {
        fpopUpBox.SetActive(true);
        fpopUpText.text = text;
        fanimator.Play("pop");
    }
    public void fPopDown()
    {
        fanimator.Play("close");
        //popUpBox.SetActive(false);
    }
    public void PopDown()
    {
        animator.Play("close");
        //popUpBox.SetActive(false);
    }

    void FlipHBox(bool faceRight)
    {      
        // Flip the character's scale based on the direction.
        Vector3 newScale = popUpBox.transform.localScale;
        newScale.x = faceRight ? .2f : -.2f;
        popUpBox.transform.localScale = newScale;

        isFacingRight = faceRight;
    }
    void FlipFBox(bool faceRight)
    {
        // Flip the character's scale based on the direction.
        Vector3 newScale = fpopUpBox.transform.localScale;
        newScale.x = faceRight ? .2f : -.2f;
        fpopUpBox.transform.localScale = newScale;

        isFacingRight = faceRight;
    }
}

