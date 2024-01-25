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

    public GameObject cpopUpBox;
    public Animator canimator;
    public TMP_Text cpopUpText;
    public string cpopUptext;

    private GameObject Hawk;
    private GameObject Fox;
    private Vector3 initialScale;
    private Quaternion initialRotation;

    public int scanreact = 0;
    private float wKeyHoldTime = 0f;
    private bool hasCounted = false;
    //private int keyHoldCounter = 0;

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
        
       Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "Lvl1 Cutscene")
        {
            if (digCount == 0)
            {
                StartCoroutine("Lvl1Cut");
                digCount++;
            }
        }

        //LEVEL 2
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
    //Cutscene Dialog
    IEnumerator Lvl1Cut()
    {
        isCoroutineRunning = true;
        yield return new WaitForSeconds(9f);

        fPopUp("Can you STOP already?!? I’m sure you noticed that if you hurt me any further, you’ll feel it just as much.");
        yield return new WaitForSeconds(5f);
        fPopUp("");

        PopUp("What did you do to me?");
        yield return new WaitForSeconds(3f);
        PopUp("");

        fPopUp("Well, I wanted to make us suffer....");
        yield return new WaitForSeconds(5f);
        fPopUp("");

        fPopUp("OBVIOUSLY I DIDN'T KNOW");
        yield return new WaitForSeconds(3f);
        fPopUp("");

        fPopUp("All right birdbrain.. all I know is that we’re connected somehow.");
        yield return new WaitForSeconds(5f);
        fPopUp("");

        PopUp("No we aren’t");
        yield return new WaitForSeconds(5f);
        PopUp("");

        fPopUp("Don’t say I didn’t warn you.");
        yield return new WaitForSeconds(5f);
        fPopUp("");

        PopUp("Then undo this you miserable mutt, You’re the one who led me here. Fix. It.");
        yield return new WaitForSeconds(5f);
        PopUp("");

        fPopUp("If you weren’t so hellbent on trying to eat me, we wouldn’t even be in this mess.");
        yield return new WaitForSeconds(5f);
        fPopUp("");


        PopUp("No if you didn’t resort to such cowardly tactics, you’d be just another quick meal.");
        yield return new WaitForSeconds(5f);

        fPopUp("");
   
        PopUp("");
     
        //Chimera appers

        cPopUp("What an amusing display…Two opposing souls are somehow linked together.");
        yield return new WaitForSeconds(5f);

        PopUp("Who or what are you?");
        yield return new WaitForSeconds(5f);


        cPopUp("You may simply call me Chimera, but a better question is who are you? A Fox or a Hawk?");
        yield return new WaitForSeconds(5f);

        fPopUp("A fox");
        PopUp("A hawk");


        cPopUp("Normally I would agree if it weren’t for the chain that keeps you linked");
        yield return new WaitForSeconds(5f);
        fPopUp("");
        PopUp("");
        //Chain Appears

        cPopUp("As long as your souls are linked, your fates are entwined, not even death can do you part.");
        yield return new WaitForSeconds(5f);

        fPopUp("Then what can?");
        yield return new WaitForSeconds(5f);

        //ANIMATION FRAME EDIT: the souls that need to be collected appear over their rightful characters
     
        cPopUp("You need to find yourselves before you can rebuild. Give before you take.");
        yield return new WaitForSeconds(5f);
        fPopUp("");
        //There will be souls out there, scattered souls that you will need to collect and give to lost beings that are called The Colorless. 
        cPopUp("There will be souls out there, scattered souls that you will need to collect and give to lost beings that are called The Colorless.");
        yield return new WaitForSeconds(5f);

        PopUp("How can we find these pieces if we’re bound together?");
        yield return new WaitForSeconds(5f);

        fPopUp("");
        PopUp("");
        //Simple, work together.
        cPopUp("Simple, work together.");
        yield return new WaitForSeconds(5f);
        //ANIMATION FRAME EDIT: The hawk and the fox look at each other and laugh, they look back to the chimera 

        fPopUp("I don’t need some birdbrain following me around.");
        yield return new WaitForSeconds(5f);

        PopUp("And I don’t need a mangy mutt slowing me down.");
        yield return new WaitForSeconds(5f);

        //Very well, good luck then
        cPopUp("Very well, good luck then");
        yield return new WaitForSeconds(5f);
        //ANIMATION FRAME EDIT: The chimera disappears and leaves the hawk and fox to complete their first-level

        digCount++;
        // PopDown();

        // fPopDown();
        StartCoroutine(PlayCloseAnimation());

        //isCoroutineRunning = false;
        //hasLvl2AirRun = true;
    } 
    IEnumerator Lvl3Cut()
    {
        //The fox and hawk are casually walking/flying, the hawk begins to say
        PopUp("Just as I thought, I don’t need you.");
        yield return new WaitForSeconds(5f);

        fPopUp("Well I didn’t need you either. I collected all my souls without a hitch.");
        yield return new WaitForSeconds(5f);

        PopUp("Yes I saw, dirtying yourself to unearth your soul fits you, mutt.");
        yield return new WaitForSeconds(5f);

        fPopUp("whatever...");
        yield return new WaitForSeconds(5f);

        //They approach a new colorless, but this time it has two sockets.
        fPopUp("That’s odd, it’s asking for two souls?");
        yield return new WaitForSeconds(5f);

        PopUp("How is it odd, just give it two of your souls so we can move on.");
        yield return new WaitForSeconds(5f);

        fPopUp("You’re stupid and colorblind? Pick a struggle. It looks like this colorless is asking for both of our souls.");
        yield return new WaitForSeconds(5f);

        PopUp("What an annoyance, let’s go");
        yield return new WaitForSeconds(5f);

        //After a while of not finding any more souls, the ability to scan is unlocked

        PopUp("Enough of this! *Scans for the first time* Damn it part of my soul is under that log.");
        yield return new WaitForSeconds(5f);

        fPopUp("Hah, look who's useless now. Observe. *Sneaks for the first time* Here’s your soul.");
        yield return new WaitForSeconds(5f);

        PopUp("…Thank you, Mutt");
        yield return new WaitForSeconds(5f);

        fPopUp("What was that birdbrain? Did you just thank me?");
        yield return new WaitForSeconds(5f);

        PopUp(" I suppose those long ears are just for show isn’t it?");
        yield return new WaitForSeconds(5f);

        fPopUp("Mhm");
        yield return new WaitForSeconds(5f);

        StartCoroutine(PlayCloseAnimation());

        //isCoroutineRunning = false;
        //hasLvl2AirRun = true;
    }
    IEnumerator Lvl5Cut()
    {
        //As they move forward, they come across a soul that’s in a high place.
        fPopUp("You have to be kidding me");
        yield return new WaitForSeconds(5f);

        PopUp("Hahaha, I see you’re still useless after all Mutt");
        yield return new WaitForSeconds(5f);

        fPopUp("Shut it. There are bound to be other souls I don’t need that one per se");
        yield return new WaitForSeconds(5f);

        //After running into more souls that are on higher ground
        fPopUp("That’s IT! Carry me");
        yield return new WaitForSeconds(5f);

        PopUp(" …What.");
        yield return new WaitForSeconds(5f);

        fPopUp("Carry. Me. If we want to move forward, I need to collect my souls and all my souls are not ground level so therefore Pick.Me.Up.");
        yield return new WaitForSeconds(5f);

        PopUp("No.");
        yield return new WaitForSeconds(5f);

        //Hawk flies away only for the chain to appear to keep it from going any further.

        PopUp("Damn this chain and damn you I refuse to carry prey that’s not for eating.");
        yield return new WaitForSeconds(5f);

        fPopUp("Then forget it.");
        yield return new WaitForSeconds(5f);

        //GIANT TEXT: I wouldn’t give up yet if I were you
        //CAMERA PANS OVER TO A RESTING CHIMERA

        PopUp("How long have you been watching us Chimera");
        yield return new WaitForSeconds(5f);

        //Chimera: Long enough to know that if you refuse to work together then it’ll only be a matter of time before you both become colorless yourselves.
        //CHIMERA FADES AWAY INTO THE BACKGROUND

        fPopUp("...");
        yield return new WaitForSeconds(5f);

        PopUp("...");
        yield return new WaitForSeconds(5f);

        fPopUp("You gonna pick me up now");
        yield return new WaitForSeconds(5f);

        PopUp("fine...");
        yield return new WaitForSeconds(5f);

        //Unlocks the pickup ability
        StartCoroutine(PlayCloseAnimation());

        //isCoroutineRunning = false;
        //hasLvl2AirRun = true;
    }
    IEnumerator Lvl7Cut()
    {
        //Seeing a ground soulless
        fPopUp("What’s that?");
        yield return new WaitForSeconds(5f);

        PopUp("I’m not sure, it’s not a colorless");
        yield return new WaitForSeconds(5f);

        fPopUp("Let’s just avoid it for now ");
        yield return new WaitForSeconds(5f);
                //Pause...
        fPopUp("By the spirits, will you just get off your high horse");
        yield return new WaitForSeconds(5f);

        PopUp("And lower myself to your level? I think not");
        yield return new WaitForSeconds(5f);

        fPopUp("You’ll get us both killed for your pride?");
        yield return new WaitForSeconds(5f);

        PopUp("Stop talking to me Mutt");
        yield return new WaitForSeconds(5f);

        //*Taunt is unlocked*
        fPopUp("Get down, you winged RAT");
        yield return new WaitForSeconds(5f);

        //*Taunted* 
        PopUp("Watch how you speak to me-");
        yield return new WaitForSeconds(5f);

        fPopUp("Shut up and stay down, Imma get us through this.");
        yield return new WaitForSeconds(5f);

       
        StartCoroutine(PlayCloseAnimation());

        //isCoroutineRunning = false;
        //hasLvl2AirRun = true;
    }

    //Passing Comments
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
        cPopDown();
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

    //Technical Methods
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
    public void cPopUp(string text)
    {
        cpopUpBox.SetActive(true);
        cpopUpText.text = text;
        canimator.Play("pop");
    }
    public void PopDown()
    {
        animator.Play("close");
        //popUpBox.SetActive(false);
    }
    public void cPopDown()
    {
        canimator.Play("close");
        //popUpBox.SetActive(false);
    }
    public void fPopDown()
    {
        fanimator.Play("close");
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
    void FlipCBox(bool faceRight)
    {
        // Flip the character's scale based on the direction.
        Vector3 newScale = cpopUpBox.transform.localScale;
        newScale.x = faceRight ? .2f : -.2f;
        cpopUpBox.transform.localScale = newScale;

        isFacingRight = faceRight;
    }
}

