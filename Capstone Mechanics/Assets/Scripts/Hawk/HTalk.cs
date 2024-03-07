using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    public AudioClip fAudio;
    public AudioClip hAudio;
    public AudioClip cAudio;
    public AudioSource MyAudioSource;

    private GameObject Hawk;
    private GameObject Fox;
    private Vector3 initialScale;
    private Quaternion initialRotation;

    public TMP_Text dialougeText;
    public Image dialougeImage;

    public Sprite CImg;
    public Sprite HImg;
    public Sprite FImg;
    public Sprite BImg;

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

    private Color fox = new Color(0.5f, 0.5f, 1.0f);   // Light blue
    private Color hawk = new Color(1.0f, 0.5f, 0.5f);  // Light red
    private Color chimera = new Color(1.0f, 0.5f, 1.0f); //purple
    // Start is called before the first frame update
    void Start()
    {
        initialScale = transform.localScale;
        initialRotation = transform.rotation;
        Hawk = GameObject.Find("Hawk");
        Fox = GameObject.Find("Fox");
        // holdnum = sayOnceDig;
        MyAudioSource = this.GetComponent<AudioSource>();
        MyAudioSource.loop = false;
    }

    // Update is called once per frame
    void Update()
    {
        
       Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        //Cutscenes
        if (sceneName == "Lvl1 Cutscene")
        {
            if (digCount == 0)
            {
                StartCoroutine("Lvl1Cut");
                digCount++;
            }
        }
        if (sceneName == "Lvl3Cutscene")
        {
            if (digCount == 0)
            {
                StartCoroutine("Lvl3Cut");
                digCount++;
            }
        }
        if (sceneName == "Lvl5Cutscene")
        {
            if (digCount == 0)
            {
                StartCoroutine("Lvl5Cut");
                digCount++;
            }
        }
        if (sceneName == "Lvl7Cutscene")
        {
            if (digCount == 0)
            {
                StartCoroutine("Lvl7Cut");
                digCount++;
            }
        }
        if (sceneName == "Lvl10Cutscene")
        {
            if (digCount == 0)
            {
                StartCoroutine("Lvl10Cut");
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

        //LEVEL9
        if (sceneName == "Lvl9")
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Count++;
            }
            if (Count == 4)
            {
                Lvl9Pass();
                Count++;
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

        dialougeText.color = fox;
        dialougeImage.sprite = FImg;
        MyAudioSource.clip = fAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        dialougeText.text = ("Can you STOP already?!? I’m sure you noticed that if you hurt me any further, you’ll feel it just as much.");
       // fPopUp("Can you STOP already?!? I’m sure you noticed that if you hurt me any further, you’ll feel it just as much.");
        yield return new WaitForSeconds(5f);
        // fPopUp("");

        dialougeText.color = hawk;
        dialougeImage.sprite = HImg;
        MyAudioSource.clip = hAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        dialougeText.text = ("What hast thou done to me?!?");
        //PopUp("What did you do to me?!?");
        yield return new WaitForSeconds(3f);
        //PopUp("");

        dialougeText.color = fox;
        dialougeImage.sprite = FImg;
        MyAudioSource.clip = fAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        dialougeText.text = ("Well, I wanted to make us suffer....");
        //fPopUp("Well, I wanted to make us suffer....");
        yield return new WaitForSeconds(5f);
        // fPopUp("");


        MyAudioSource.clip = fAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        dialougeText.text = ("OBVIOUSLY I DIDN'T KNOW!");
        //fPopUp("OBVIOUSLY I DIDN'T KNOW!");
        yield return new WaitForSeconds(3f);
        // fPopUp("");


        MyAudioSource.clip = fAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        dialougeText.text = ("All I know birdbrain is that we’re connected somehow.");
        //fPopUp("All I know birdbrain is that we’re connected somehow.");
        yield return new WaitForSeconds(5f);
        //fPopUp("");
        dialougeText.color = hawk;
        dialougeImage.sprite = HImg;
        MyAudioSource.clip = hAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        dialougeText.text = ("Nay, we are not.");
        //PopUp("No we can't be..");
        yield return new WaitForSeconds(5f);
        //PopUp("");

        dialougeText.color = fox;
        dialougeImage.sprite = FImg;
        MyAudioSource.clip = fAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        dialougeText.text = ("Don’t say I didn’t warn you.");
        //fPopUp("Don’t say I didn’t warn you.");
        yield return new WaitForSeconds(5f);
        // fPopUp("");
        dialougeText.color = hawk;
        dialougeImage.sprite = HImg;
        MyAudioSource.clip = hAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        dialougeText.text = ("Undo this plight Mutt, thou art the guide who brought me hither. Mend it.");
        //PopUp("Then undo this you mutt, You’re the one who led me here. Fix. It.");
        yield return new WaitForSeconds(5f);
        // PopUp("");

        dialougeText.color = fox;
        dialougeImage.sprite = FImg;
        MyAudioSource.clip = fAudio;
        MyAudioSource.pitch = UnityEngine.Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        dialougeText.text = ("If you weren’t so hellbent on trying to eat me, we wouldn’t even be in this mess.");
        //fPopUp("If you weren’t so hellbent on trying to eat me, we wouldn’t even be in this mess.");
        yield return new WaitForSeconds(5f);
        //fPopUp("");

        dialougeText.color = hawk;
        dialougeImage.sprite = HImg;
        MyAudioSource.clip = hAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        dialougeText.text = ("Nay, if thou hadst not resorted to such craven tactics, thou wouldst be just another meal.");
        //PopUp("No if you didn’t resort to such cowardly tactics, you’d be just another meal.");
        yield return new WaitForSeconds(5f);

        //fPopUp("");

        //PopUp("");

        //Chimera appers
        dialougeText.color = chimera;
        dialougeImage.sprite = null;
        MyAudioSource.clip = cAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        dialougeText.text = ("What an amusing display…");
       // cPopUp("What an amusing display…");
        yield return new WaitForSeconds(2f);
        dialougeText.text = ("Two opposing souls are somehow linked together.");
       // cPopUp("Two opposing souls are somehow linked together.");
        yield return new WaitForSeconds(3f);

        dialougeText.color = hawk;
        dialougeImage.sprite = HImg;
        MyAudioSource.clip = hAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        dialougeText.text = ("Who or what art thou?");
        //PopUp("Who or what are you?");
        yield return new WaitForSeconds(5f);

        dialougeText.color = chimera;
        dialougeImage.sprite = CImg;
        MyAudioSource.clip = cAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        dialougeText.text = ("You may simply call me Chimera, but a better question is who are you? A Fox or a Hawk?");
        //cPopUp("You may simply call me Chimera, but a better question is who are you? A Fox or a Hawk?");
        yield return new WaitForSeconds(5f);

        dialougeImage.sprite = null;
        //dialougeText.color = fox;
        dialougeImage.sprite = BImg;

        dialougeText.text = ("<color=#7F7FFF> A Fox! </color>\n" + "<color=#FF7F7F>A Hawk! </color>");
        //fPopUp("A fox");
        // PopUp("A hawk");
        yield return new WaitForSeconds(2f);

        dialougeText.color = chimera;
        dialougeImage.sprite = CImg;
        MyAudioSource.clip = cAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        dialougeText.text = ("Normally I would agree if it weren’t for the chain that keeps you linked");
        //cPopUp("Normally I would agree if it weren’t for the chain that keeps you linked");
        yield return new WaitForSeconds(5f);
        //fPopUp("");
        //PopUp("");
        //Chain Appears

        dialougeText.text = ("As long as your souls are linked, your fates are entwined, not even death can do you part.");
       // cPopUp("As long as your souls are linked, your fates are entwined, not even death can do you part.");
        yield return new WaitForSeconds(5f);

        dialougeText.color = fox;
        dialougeImage.sprite = FImg;
        MyAudioSource.clip = fAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        dialougeText.text = ("Then what can?");
        //fPopUp("Then what can?");
        yield return new WaitForSeconds(5f);

        //ANIMATION FRAME EDIT: the souls that need to be collected appear over their rightful characters
        dialougeText.color = chimera;
        dialougeImage.sprite = CImg;
        MyAudioSource.clip = cAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        dialougeText.text = ("You need to find yourselves before you can rebuild. Give before you take.");
        //cPopUp("You need to find yourselves before you can rebuild. Give before you take.");
        yield return new WaitForSeconds(5f);
       // fPopUp("");
        //There will be souls out there, scattered souls that you will need to collect and give to lost beings that are called The Colorless.
        dialougeText.text = ("There will be souls out there, scattered souls that you will need to collect and give to lost beings that are called The Colorless.");
        //cPopUp("There will be souls out there, scattered souls that you will need to collect and give to lost beings that are called The Colorless.");
        yield return new WaitForSeconds(5f);

        dialougeText.color = hawk;
        dialougeImage.sprite = HImg;
        MyAudioSource.clip = hAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        dialougeText.text = ("How can we find these fragments if we be entwined?");
        //PopUp("How can we find these pieces if we’re bound together?");
        yield return new WaitForSeconds(5f);

        //fPopUp("");
        //PopUp("");
        //Simple, work together.
        dialougeText.color = chimera;
        dialougeImage.sprite = CImg;
        MyAudioSource.clip = cAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        dialougeText.text = ("Simple, work together.");
        //cPopUp("Simple, work together.");
        yield return new WaitForSeconds(5f);
        //ANIMATION FRAME EDIT: The hawk and the fox look at each other and laugh, they look back to the chimera 

        dialougeText.color = fox;
        dialougeImage.sprite = FImg;
        MyAudioSource.clip = fAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        dialougeText.text = ("I don’t need some rat with wings following me around.");
        //fPopUp("I don’t need some birdbrain following me around.");
        yield return new WaitForSeconds(5f);

        dialougeText.color = hawk;
        dialougeImage.sprite = HImg;
        MyAudioSource.clip = hAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        dialougeText.text = ("And I need not a scrawny cur hindering my pace.");
        //PopUp("And I don’t need a mangy mutt slowing me down.");
        yield return new WaitForSeconds(5f);

        //Very well, good luck then
        dialougeText.color = chimera;
        dialougeImage.sprite = CImg;
        MyAudioSource.clip = cAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        dialougeText.text = ("Very well, good luck then");
        //cPopUp("Very well, good luck then");
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
        dialougeText.color = hawk;
        dialougeImage.sprite = HImg;
        MyAudioSource.clip = hAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        dialougeText.text = ("Just as I reckon, I have no need of thee.");
       //PopUp();
        yield return new WaitForSeconds(5f);

        dialougeText.color = fox;
        dialougeImage.sprite = FImg;
        MyAudioSource.clip = fAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        dialougeText.text = ("Well, I didn't need youse either. I collected all my souls without a hitch.");
        //fPopUp();
        yield return new WaitForSeconds(5f);

        dialougeText.color = hawk;
        dialougeImage.sprite = HImg;
        dialougeText.text = ("Aye, I beheld it, soiling thyself to uncover thy soul befits thee, thou cur.");
        //PopUp();
        yield return new WaitForSeconds(5f);

        dialougeText.color = fox;
        dialougeImage.sprite = FImg;
        MyAudioSource.clip = fAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        dialougeText.text = ("whateva.");
       // fPopUp();
        yield return new WaitForSeconds(10f);


        MyAudioSource.clip = fAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        //They approach a new colorless, but this time it has two sockets.
        dialougeText.text = ("That's odd, it's askin' for two souls?");
        //fPopUp();
        yield return new WaitForSeconds(5f);

        dialougeText.color = hawk;
        dialougeImage.sprite = HImg;
        dialougeText.text = ("How is it strange? Just render it two of thy souls, so we may proceed.");
        //PopUp();
        yield return new WaitForSeconds(5f);

        dialougeText.color = fox;
        dialougeImage.sprite = FImg;
        MyAudioSource.clip = fAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        dialougeText.text = ("You're stupid and colorblind? It looks like this colorless is askin' for both of our souls.");
        //fPopUp();
        yield return new WaitForSeconds(5f);

        //After a while of not finding any more souls, the ability to scan is unlocked
        dialougeText.color = hawk;
        dialougeImage.sprite = HImg;
        MyAudioSource.clip = hAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        dialougeText.text = ("What a vexation, let us away.");
        yield return new WaitForSeconds(7f);
        dialougeText.text = ("Damn it, part of my soul is beneath the earth.");
        //PopUp();
        yield return new WaitForSeconds(3f);

        dialougeText.color = fox;
        dialougeImage.sprite = FImg;
        MyAudioSource.clip = fAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        dialougeText.text = ("Hah, look who's useless now. Observe.");
        yield return new WaitForSeconds(7f);
        dialougeText.text = ("Here's ya soul.");
        //fPopUp("Hah, look who's useless now. Observe. *Sneaks for the first time* Here’s your soul.");
        yield return new WaitForSeconds(3f);

        dialougeText.color = hawk;
        dialougeImage.sprite = HImg;
        MyAudioSource.clip = hAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        dialougeText.text = ("…Thank thee, Mutt.");
        //PopUp();
        yield return new WaitForSeconds(5f);

        dialougeText.color = fox;
        dialougeImage.sprite = FImg;
        MyAudioSource.clip = fAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        dialougeText.text = ("What was that, birdbrain? Did ya just thank me?");
        //fPopUp();
        yield return new WaitForSeconds(5f);

        dialougeText.color = hawk;
        dialougeImage.sprite = HImg;
        MyAudioSource.clip = hAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        dialougeText.text = ("I suppose those lengthy ears are merely for display, art they not?");
        //PopUp();
        yield return new WaitForSeconds(5f);

        dialougeText.color = fox;
        dialougeImage.sprite = FImg;
        MyAudioSource.clip = fAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        dialougeText.text = ("*Grumbles*");
        //fPopUp();
        yield return new WaitForSeconds(5f);

        StartCoroutine(PlayCloseAnimation());

        //isCoroutineRunning = false;
        //hasLvl2AirRun = true;
    }
    IEnumerator Lvl5Cut()
    {
        //As they move forward, they come across a soul that’s in a high place.
        yield return new WaitForSeconds(5f);

        dialougeText.color = fox;
        dialougeImage.sprite = FImg;
        MyAudioSource.clip = fAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        dialougeText.text = ("You gotta be kiddin' me.");
        //fPopUp("You have to be kidding me");
        yield return new WaitForSeconds(5f);

        dialougeText.color = hawk;
        dialougeImage.sprite = HImg;
        MyAudioSource.clip = hAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        dialougeText.text = ("Hahaha, I perceive thou art still ineffectual, Mutt.");
        //PopUp("Hahaha, I see you’re still useless after all Mutt");
        yield return new WaitForSeconds(5f);

        dialougeText.color = fox;
        dialougeImage.sprite = FImg;
        MyAudioSource.clip = fAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        dialougeText.text = ("Shaddap. There are bound to be other souls. I don't need that one.");
        //fPopUp("");
        yield return new WaitForSeconds(10f);


        MyAudioSource.clip = fAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        //After running into more souls that are on higher ground
        dialougeText.text = ("That’s IT! Carry me");
        //fPopUp("That’s IT! Carry me");
        yield return new WaitForSeconds(5f);

        dialougeText.color = hawk;
        dialougeImage.sprite = HImg;
        MyAudioSource.clip = hAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        dialougeText.text = (" …What.");
        //PopUp("");
        yield return new WaitForSeconds(5f);

        dialougeText.color = fox;
        dialougeImage.sprite = FImg;
        MyAudioSource.clip = fAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        dialougeText.text = ("If we wanna move forward, I gotta collect my souls, and all my souls ain't ground level, so therefore, Pick. Me. Up.");
        //fPopUp("");
        yield return new WaitForSeconds(5f);

        dialougeText.color = hawk;
        dialougeImage.sprite = HImg;
        MyAudioSource.clip = hAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        dialougeText.text = ("No.");
       // PopUp("");
        yield return new WaitForSeconds(2f);


        MyAudioSource.clip = hAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        //Hawk flies away only for the chain to appear to keep it from going any further.
        dialougeText.text = ("Damn this chain, and damn thee! I refuse to bear quarry unfit for consumption.");
       // PopUp("");
        yield return new WaitForSeconds(5f);

        dialougeText.color = fox;
        dialougeImage.sprite = FImg;
        MyAudioSource.clip = fAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        dialougeText.text = ("Then forget it.");
        //fPopUp("");
        yield return new WaitForSeconds(5f);

        //GIANT TEXT: 
        //CAMERA PANS OVER TO A RESTING CHIMERA

        dialougeText.color = chimera;
        dialougeImage.sprite = CImg;
        MyAudioSource.clip = cAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        dialougeText.text = ("I wouldn’t give up yet if I were you");
        yield return new WaitForSeconds(5f);

        dialougeText.color = hawk;
        dialougeImage.sprite = HImg;
        MyAudioSource.clip = hAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        dialougeText.text = (" For how long hast thou been observing us, Chimera?");
        //PopUp("");
        yield return new WaitForSeconds(5f);

        //Chimera:
        dialougeText.color = chimera;
        dialougeImage.sprite = CImg;
        MyAudioSource.clip = cAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        dialougeText.text = (" Long enough to know that if you refuse to work together then it’ll only be a matter of time before you both become colorless yourselves.");
        yield return new WaitForSeconds(5f);
        //CHIMERA FADES AWAY INTO THE BACKGROUND

        dialougeText.color = fox;
        dialougeImage.sprite = FImg;
        dialougeText.text = ("...");
        //fPopUp("");
        yield return new WaitForSeconds(2f);

        dialougeText.color = hawk;
        dialougeImage.sprite = HImg;
        dialougeText.text = ("...");
        //PopUp("...");
        yield return new WaitForSeconds(2f);

        dialougeText.color = fox;
        dialougeImage.sprite = FImg;
        MyAudioSource.clip = fAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        dialougeText.text = ("Ya gonna pick me up now?");
        //fPopUp("");
        yield return new WaitForSeconds(5f);

        dialougeText.color = hawk;
        dialougeImage.sprite = HImg;
        MyAudioSource.clip = hAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        dialougeText.text = ("fine...");
        //PopUp("");
        yield return new WaitForSeconds(5f);

        //Unlocks the pickup ability
        StartCoroutine(PlayCloseAnimation());

        //isCoroutineRunning = false;
        //hasLvl2AirRun = true;
    }
    IEnumerator Lvl7Cut()
    {
        //Seeing a ground soulless
        dialougeText.color = fox;
        dialougeImage.sprite = FImg;
        MyAudioSource.clip = fAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        dialougeText.text = ("What’s that?");
        //fPopUp("What’s that?");
        yield return new WaitForSeconds(5f);

        dialougeText.color = hawk;
        dialougeImage.sprite = HImg;
        MyAudioSource.clip = hAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        dialougeText.text = ("I’m not sure, it’s definetly not a colorless");
        //PopUp("I’m not sure, it’s not a colorless");
        yield return new WaitForSeconds(5f);

        dialougeText.color = fox;
        dialougeImage.sprite = FImg;
        MyAudioSource.clip = fAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        dialougeText.text = ("Let’s just avoid it for now");
        //fPopUp("Let’s just avoid it for now..");
        yield return new WaitForSeconds(8f);
        //Pause...

        MyAudioSource.clip = fAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        dialougeText.text = ("By the spirits, will you just get off your high horse");
        //fPopUp("");
        yield return new WaitForSeconds(5f);

        dialougeText.color = hawk;
        dialougeImage.sprite = HImg;
        MyAudioSource.clip = hAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        dialougeText.text = ("And put myself to your level? I think not");
        //PopUp("");
        yield return new WaitForSeconds(5f);

        dialougeText.color = fox;
        dialougeImage.sprite = FImg;
        MyAudioSource.clip = fAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        dialougeText.text = ("You’ll get us both killed for your pride?");
       // fPopUp("");
        yield return new WaitForSeconds(5f);

        dialougeText.color = hawk;
        dialougeImage.sprite = HImg;
        MyAudioSource.clip = hAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        dialougeText.text = ("Stop talking to me Mutt");
        //PopUp("Stop talking to me Mutt");
        yield return new WaitForSeconds(5f);

        //*Taunt is unlocked*
        dialougeText.color = fox;
        dialougeImage.sprite = FImg;
        MyAudioSource.clip = fAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        dialougeText.text = ("Get down, you winged RAT");
        //fPopUp("");
        yield return new WaitForSeconds(5f);

        //*Taunted* 
        dialougeText.color = hawk;
        dialougeImage.sprite = HImg;
        MyAudioSource.clip = hAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        dialougeText.text = ("WATCH YOUR TOUNGUE-");
        //PopUp("");
        yield return new WaitForSeconds(2f);

        dialougeText.color = fox;
        dialougeImage.sprite = FImg;
        MyAudioSource.clip = fAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        dialougeText.text = ("Just shut up and stay down! Imma get us through this.");
        //fPopUp("");
        yield return new WaitForSeconds(5f);

       
        StartCoroutine(PlayCloseAnimation());

        //isCoroutineRunning = false;
        //hasLvl2AirRun = true;
    }
    IEnumerator Lvl10Cut()
    {
        dialougeText.color = hawk;
        dialougeImage.sprite = HImg;
        dialougeText.text = ("When will we be released Chimera");
        //PopUp("");
        yield return new WaitForSeconds(3f);

        dialougeText.color = fox;
        dialougeImage.sprite = FImg;
        dialougeText.text = ("I was wondering the same thing, ya know?");
        yield return new WaitForSeconds(5f);

        dialougeText.color = chimera;
        dialougeImage.sprite = CImg;
        dialougeText.text = ("*Chuckles* I only have, but a single question");
        yield return new WaitForSeconds(5f);

        dialougeImage.sprite = BImg;
        dialougeText.text = ("<color=#7F7FFF> What </color> /n" + "<color=#FF7F7F> What </color>");
        yield return new WaitForSeconds(2f);

        dialougeText.color = chimera;
        dialougeImage.sprite = CImg;
        dialougeText.text = ("Were you able to do this alone?");
        yield return new WaitForSeconds(8f);

        dialougeImage.sprite = BImg;
        dialougeText.text = ("<color=#7F7FFF> No </color>/n" + "<color=#FF7F7F> No </color>");
        yield return new WaitForSeconds(4f);

        dialougeText.color = chimera;
        dialougeImage.sprite = CImg;
        dialougeText.text = ("Good, there will be no colorless here, instead you will be coming to me. I need, but only one soul of each. Do that and you shall be set free.");
        yield return new WaitForSeconds(8f);

        dialougeText.color = fox;
        dialougeImage.sprite = FImg;
        dialougeText.text = ("Well birdbrain, I guess this is it. Let's end this.");
        yield return new WaitForSeconds(5f);

        dialougeText.color = hawk;
        dialougeImage.sprite = HImg;
        dialougeText.text = ("Wait…before we go…my name is <b><i> .... </i></b>");
        yield return new WaitForSeconds(3f);

        dialougeText.color = fox;
        dialougeImage.sprite = FImg;
        dialougeText.text = ("*Chuckles* Nice to meet you, my name is <b><i> .... </i></b>");
        yield return new WaitForSeconds(3f);

        dialougeText.color = hawk;
        dialougeImage.sprite = HImg;
        dialougeText.text = ("Now may we proceed. ");
        yield return new WaitForSeconds(2f);

        dialougeText.color = fox;
        dialougeImage.sprite = FImg;
        dialougeText.text = ("Lets finish this");
        yield return new WaitForSeconds(5f);
        StartCoroutine(PlayCloseAnimation());
    }

    //Passing Comments
    IEnumerator Lvl2Air()
    {
        isCoroutineRunning = true;
        MyAudioSource.clip = fAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        fPopUp("Stop flyin' so high, before youse kill us both, birdbrain.");
        yield return new WaitForSeconds(5f);

        MyAudioSource.clip = hAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        PopUp("If I could, I would cast thee from the loftiest summit.");
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

        MyAudioSource.clip = hAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        PopUp("Canst thou dig any swifter?!?");

        yield return new WaitForSeconds(5f);

        MyAudioSource.clip = fAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
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

        MyAudioSource.clip = fAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        fPopUp("So ya tellin' me you had the ability to see all the orbs this entire time and just now usin' it?");
        yield return new WaitForSeconds(5f);


        MyAudioSource.clip = hAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        PopUp("...Shortly, aye; lengthily, nay");
        yield return new WaitForSeconds(5f);

        MyAudioSource.clip = fAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        fPopUp(" I hate you");
        yield return new WaitForSeconds(5f);

        digCount++;
        // Play the close animation
        StartCoroutine(PlayCloseAnimation());
       
        isCoroutineRunning = false;
    }
    IEnumerator Lvl4Sneak()
    {
        isCoroutineRunning = true;

        MyAudioSource.clip = hAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        PopUp("Thou must be some manner of cat.");
        yield return new WaitForSeconds(5f);

        MyAudioSource.clip = fAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        fPopUp("What !?! No I’m not");
        yield return new WaitForSeconds(3f);

        MyAudioSource.clip = hAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
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

        MyAudioSource.clip = hAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        PopUp("If it’s any consolation, I hate this too..");

        yield return new WaitForSeconds(5f);

        MyAudioSource.clip = fAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        fPopUp("I hate you.");
        yield return new WaitForSeconds(5f);

        

        // Play the close animation
        StartCoroutine(PlayCloseAnimation());

        isCoroutineRunning = false;
    }
    IEnumerator Lvl6Drop()
    {
        isCoroutineRunning = true;

        MyAudioSource.clip = fAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        fPopUp("OW!?!");
        yield return new WaitForSeconds(3f);

        MyAudioSource.clip = hAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        PopUp("Oops..");
        yield return new WaitForSeconds(3f);

        MyAudioSource.clip = fAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        fPopUp("Yknow this hurts you too right??");
        yield return new WaitForSeconds(5f);

        MyAudioSource.clip = hAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
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

        MyAudioSource.clip = fAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        fPopUp("Are you allergic to the ground?");
        yield return new WaitForSeconds(3f);

        MyAudioSource.clip = hAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        PopUp("What are you on about?");
        yield return new WaitForSeconds(3f);

        MyAudioSource.clip = fAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        fPopUp("You're just always flying, not once have you landed");
        yield return new WaitForSeconds(5f);

        MyAudioSource.clip = fAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        fPopUp("Do your wings not hurt??");
        yield return new WaitForSeconds(4f);

        MyAudioSource.clip = hAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        PopUp("no...?");
        yield return new WaitForSeconds(3f);

        //digCount++;
        Lvl8Play = true;
        // Play the close animation
        StartCoroutine(PlayCloseAnimation());

        isCoroutineRunning = false;
    }
    IEnumerator Lvl9Pass()
    {
        isCoroutineRunning = true;

        MyAudioSource.clip = hAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        PopUp("Pray tell, doth filth truly not vex thee?!");
        yield return new WaitForSeconds(3f);

        MyAudioSource.clip = fAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        fPopUp("You're still on this??");
        yield return new WaitForSeconds(3f);

        MyAudioSource.clip = hAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        PopUp("Yes, dost thou not yearn for the ability to soar through the heavens?!");
        yield return new WaitForSeconds(3f);

        MyAudioSource.clip = fAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        fPopUp("Absolutely NOT.");
        yield return new WaitForSeconds(5f);

        MyAudioSource.clip = hAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        PopUp("WHY NOT");
        yield return new WaitForSeconds(4f);

        MyAudioSource.clip = fAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        fPopUp("I’M SCARED OF HEIGHTS");
        yield return new WaitForSeconds(3f);

        PopUp("...");
        fPopUp("...");
        yield return new WaitForSeconds(4f);

        MyAudioSource.clip = hAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        PopUp("This whole time-");
        yield return new WaitForSeconds(2f);

        MyAudioSource.clip = hAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        PopUp("Shut Up");
        yield return new WaitForSeconds(4f);
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

        MyAudioSource.clip = hAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        PopUp("Stupid Fox! Can you touch the ground!?");
            //yield return new WaitForSeconds(5f);
            //PopDown();
        }
    IEnumerator canScanNeu()
    {
        yield return new WaitForSeconds(3f);

        MyAudioSource.clip = hAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
        PopUp("Hey Fox, can you reach the ground?");
        //yield return new WaitForSeconds(5f);
        //PopDown();
    }
    IEnumerator canScanPos()
    {
        yield return new WaitForSeconds(3f);

        MyAudioSource.clip = hAudio;
        MyAudioSource.pitch = Random.Range(1f, 1.5f);
        MyAudioSource.Play();
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

