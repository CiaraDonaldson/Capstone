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
    void Start()
    {
         c1 = GameObject.FindWithTag("colorsplit1");
         c2 = GameObject.FindWithTag("colorsplit2");

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
                    Debug.Log("Nothings here");
                }
            }

        }
    }

    // Update is called once per frame
    void Update()
    {          
       
        if (fText == null)
        {
            Debug.Log(" ");
        }
        else
        {
            fText.text = fOrbs.ToString();
        }
        if (hText == null)
        {
            Debug.Log(" ");
        }
        else
        {
            hText.text = hOrbs.ToString();
        }
        if (fText2 == null)
        {
            Debug.Log(" ");
        }
        else
        {
            fText2.text = fOrbs.ToString();
        }
        if (hText2 == null)
        {
            Debug.Log(" ");
        }
        else
        {
            hText2.text = hOrbs.ToString();
        }

        if (c1.GetComponent<ColorlessSplitInt>().count == 1 && c2.GetComponent<OtherColorlessSplitInt>().count == 1)
        {

            c1.GetComponent<ColorlessSplitInt>().myEvent.Invoke();
            //c1.GetComponent<ColorlessSplitInt>().count++;

            c2.GetComponent<OtherColorlessSplitInt>().myEvent.Invoke();
            //c2.GetComponent<OtherColorlessSplitInt>().count++;


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

        SceneManager.LoadScene("Credits");
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Fox" || collision.gameObject.name == "Hawk")
        {
            collision.transform.Translate(Vector3.up * 3f);

        }
    }
   
}
