
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Colorlessinteract : MonoBehaviour
{
    public string popUp;
    public int fOrbCount = 3;
    public int hOrbCount = 2;
    public bool pass = false;
    public GameObject popUpBox;
    public Animator animator;
    public TMP_Text popUpText;
    public AudioSource Audio;
    private int count = 0;
    bool changingColor = false;
    public GameObject GameManager;

    private void Start()
    {
        GameManager = GameObject.Find("GameManager");
    }
    private void Update()
    {
        if (count == 1)
        {
            Color lilacColor = new Color(0.8f, 0.6f, 1.0f);
            StartCoroutine(LerpColor(this.GetComponent<MeshRenderer>(), Color.white, lilacColor, 1.5f));
            StartCoroutine(PopUpAndSwitch());
            count++;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Hawk" | other.gameObject.name == "Fox")
        {
            PopUp(popUp);
        }

        if (GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().fOrbs == fOrbCount && GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().hOrbs == hOrbCount)
        {
            count++;
        }

        if (GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().fOrbs > fOrbCount | GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().hOrbs > hOrbCount)
        {
            StartCoroutine(TooMany());
        }
    }
    private void loadNextScene()
    {
        if (!GameObject.Find("StoryMode"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (GameObject.Find("StoryMode"))
        {
            if (SceneManager.GetActiveScene().name == "Lvl2")
            {
                SceneManager.LoadScene("Lvl3Cutscene");
            }
            if (SceneManager.GetActiveScene().name == "Lvl4")
            {
                GameManager.GetComponent<SceneSwitch>().Lvl5Cutscene();
            }
            if (SceneManager.GetActiveScene().name == "Lvl6")
            {
                SceneManager.LoadScene("Lvl7Cutscene");
            }
            if (SceneManager.GetActiveScene().name != "Lvl2" || SceneManager.GetActiveScene().name != "Lvl4" || SceneManager.GetActiveScene().name != "Lvl6")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
       
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PopDown();
        }
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
    IEnumerator TooMany()
    {
        PopUp("This is way too many!");
        yield return new WaitForSeconds(2f);
        PopUp("You need to bring me exactly " + fOrbCount + "Fox Orbs and " + hOrbCount + "Hawk Orbs, or I cant restore :[");
    }
    IEnumerator PopUpAndSwitch()
    {
        PopUp("You Did It! Thank you!");

        // Wait for a certain duration before changing the color
        //yield return new WaitForSeconds(2f); // Adjust the duration as needed
        Audio.Play();
       
        pass = true;

        // Wait for another duration before switching scenes
        yield return new WaitForSeconds(3f); // Adjust the duration as needed

        loadNextScene();
    }
    IEnumerator LerpColor(MeshRenderer mesh, Color fromColor, Color toColor, float duration)
    {
        if (changingColor)
        {
            yield break;
        }
        changingColor = true;
        float counter = 0;

        while (counter < duration)
        {
            counter += Time.deltaTime;

            float colorTime = counter / duration;
           // Debug.Log(colorTime);

            //Change color
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(fromColor, toColor, counter / duration);
            //Wait for a frame
            yield return null;
        }
        changingColor = false;
    }
}