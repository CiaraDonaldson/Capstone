using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI fText;
    public TextMeshProUGUI hText;
    public TextMeshProUGUI fText2;
    public TextMeshProUGUI hText2;
    public int hOrbs = 0;
    public int fOrbs = 0;
    public Canvas pauseMenuUI;
    public GameObject Story;

    public GameObject popUpBox;
    public Animator animator;
    public TMP_Text popUpText;
    public string popUptext;

    [SerializeField] private GameObject c1;
    [SerializeField] private GameObject c2;
    // Start is called before the first frame update
    void Awake()
    {
        if (SceneManager.GetActiveScene().name.ToString() == "LivingWorld" || SceneManager.GetActiveScene().name.ToString() == "LivingWorldEnd" || SceneManager.GetActiveScene().name.ToString() == "Lvl1 Cutscene" || SceneManager.GetActiveScene().name.ToString() == "Lvl3Cutscene" || SceneManager.GetActiveScene().name.ToString() == "Lvl5Cutscene" || SceneManager.GetActiveScene().name.ToString() == "Lvl7Cutscene" || SceneManager.GetActiveScene().name.ToString() == "Lvl10Cutscene")
        {
            this.GetComponent<circleLoad>().enabled = true;

        }
        else
        {
            this.GetComponent<circleLoad>().enabled = false;
        }
        if (SceneManager.GetActiveScene().buildIndex >= 0 && SceneManager.GetActiveScene().buildIndex <= 13)
        {
            this.GetComponent<PlayerHealth>().enabled = false;
            if (GameObject.Find("Fox"))
            {
                if (GameObject.Find("Fox").GetComponent<FoxController>())
                {
                    GameObject.Find("Fox").GetComponent<FoxController>().enabled = false;
                }
            }
            if (GameObject.Find("Hawk"))
            {
                if (GameObject.Find("Hawk").GetComponent<Scan>())
                {
                    GameObject.Find("Hawk").GetComponent<Scan>().enabled = false;
                }
            }
        }
        else
        {
            this.GetComponent<PlayerHealth>().enabled = true;
            if (GameObject.Find("Fox"))
            {
                GameObject.Find("Fox").GetComponent<FoxController>().enabled = true;
                //GameObject.Find("Fox").GetComponent<Taunt>().enabled = true;

            }
            if (GameObject.Find("Hawk"))
            {
                GameObject.Find("Hawk").GetComponent<Scan>().enabled = true;
            }
        }

    }
    void Start()
    {
        hOrbs = 0;
        fOrbs = 0;

        c1 = GameObject.Find("ColourlessSplit/Colorless");
         c2 = GameObject.Find("OtherColourlessSplit/Colorless");

        Story = GameObject.Find("StoryMode");

      
            if (SceneManager.GetActiveScene().name.ToString() == "Lvl3Cutscene")
        {
            animator.Play("Pop");
            //animator.Play("Idle");
        }
        else
        {
            if (SceneManager.GetActiveScene().name.ToString() != "Lvl3Cutscene")
            {
                if (popUpBox == null & animator == null & popUpText == null)
                {
                    //nothing
                }
            }

        }
    }

    // Update is called once per frame
    void Update()
    {

        if (fText == null | hText == null | fText2 == null | hText2 == null)
        {
            //nothing
        }
        else
        {
            fText.text = fOrbs.ToString();
            hText.text = hOrbs.ToString();
            fText2.text = fOrbs.ToString();
            hText2.text = hOrbs.ToString();
        }

        if (c1 != null && c2 != null)
        {
            if (GameObject.Find("ColourlessSplit/Colorless").GetComponent<ColorlessSplitInt>().count >= 1 && GameObject.Find("OtherColourlessSplit/Colorless").GetComponent<OtherColorlessSplitInt>().count >= 1) 
        {

                c1.GetComponent<ColorlessSplitInt>().myEvent.Invoke();
                //c1.GetComponent<ColorlessSplitInt>().count++;

                c2.GetComponent<OtherColorlessSplitInt>().myEvent.Invoke();
                //c2.GetComponent<OtherColorlessSplitInt>().count++;

            }
        }
       // string sceneName = SceneManager.GetActiveScene().ToString();

       /* if (sceneName != "Title" | !pauseMenuUI.enabled)
        {
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }*/
    }
    public void restartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
    public void startGamemode()
    {
        // Cursor.visible = true;
            SceneManager.LoadScene("GameMode");
    }
    public void startArcade()
    {
        // Cursor.visible = true;
        GameObject.Find("StoryMode").GetComponent<DoNotDestroy>().enabled = false;
        Destroy(GameObject.Find("StoryMode"));
        SceneManager.LoadScene("Lvl1");
    }
    public void startStory()
    {
        // Cursor.visible = true;
        GameObject.Find("StoryMode").GetComponent<DoNotDestroy>().enabled = true;
        SceneManager.LoadScene("LivingWorld");
    }
    public void startCredit()
    {
       // Cursor.visible = true;

        SceneManager.LoadScene("Scroll Credits");
    }
    public void startOptions()
    {
       // Cursor.visible = true;

        SceneManager.LoadScene("Options");
    }
    public void startControls()
    {
      //  Cursor.visible = true;

        SceneManager.LoadScene("Controls");
    }
    public void addHOrb()
    {
        hOrbs = hOrbs + 1;
    }
    public void addFOrb()
    {
        fOrbs = fOrbs + 1;
    }
      
}
