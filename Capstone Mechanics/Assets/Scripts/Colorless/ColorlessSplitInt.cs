using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using TMPro;

public class ColorlessSplitInt: MonoBehaviour
{
    public string popUp;
    public int hOrbCount = 2;
    public bool pass = false;

    public GameObject popUpBox;
    public Animator animator;
    public TMP_Text popUpText;

    public AudioSource Audio;
    public int count = 0;
    bool changingColor = false;

    public UnityEvent myEvent = null;


  
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Hawk")
        {
            PopUp(popUp);
        }

        if (GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().hOrbs == hOrbCount)
        {
            count++;
        }

        if (GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().hOrbs > hOrbCount)
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

        if (GameObject.Find("StoryMode") && SceneManager.GetActiveScene().name == "Lvl2")
        {
            SceneManager.LoadScene("Lvl3Cutscene");
        }
        else if (GameObject.Find("StoryMode") && SceneManager.GetActiveScene().name == "Lvl4")
        {
            SceneManager.LoadScene("Lvl5Cutscene");
        }
        else if (GameObject.Find("StoryMode") && SceneManager.GetActiveScene().name == "Lvl6")
        {
            SceneManager.LoadScene("Lvl7Cutscene");
        }
        else if (GameObject.Find("StoryMode") && SceneManager.GetActiveScene().name != "Lvl2" || SceneManager.GetActiveScene().name != "Lvl4" || SceneManager.GetActiveScene().name != "Lvl6")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
        PopUp("You need to bring me the exact amount, or I cant restore :[");
    }
    public void EndLvl()
    {
        StartCoroutine(PopUpAndSwitch());
        StartCoroutine(LerpColor(this.GetComponent<MeshRenderer>(), Color.white, new Color(0.8f, 0.6f, 1.0f), 1.5f));
    }
    public IEnumerator PopUpAndSwitch()
    {
        PopUp("You Did It! Thank you!");

        // Wait for a certain duration before changing the color
        //yield return new WaitForSeconds(2f); // Adjust the duration as needed
        Audio.Play();
       
        pass = true;

        // Wait for another duration before switching scenes
        yield return new WaitForSeconds(4f); // Adjust the duration as needed

        loadNextScene();
    }
    public IEnumerator LerpColor(MeshRenderer mesh, Color fromColor, Color toColor, float duration)
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
            Debug.Log(colorTime);

            //Change color
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(fromColor, toColor, counter / duration);
            //Wait for a frame
            yield return null;
        }
        changingColor = false;
    }
}
