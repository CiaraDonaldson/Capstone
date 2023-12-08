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
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Hawk" | other.gameObject.name == "Fox")
        {
            PopUp(popUp);
        }

        if (GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().fOrbs == fOrbCount && GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().hOrbs == hOrbCount)
        {
            StartCoroutine(PopUpAndSwitch());
        }
    }
    private void loadNextScene()
    {
        // int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
    IEnumerator PopUpAndSwitch()
    {
        PopUp("You Did It! Thank you!");

        // Wait for a certain duration before changing the color
        yield return new WaitForSeconds(2f); // Adjust the duration as needed

        Color lerpedColor = Color.Lerp(Color.white, Color.magenta, 20f);
        this.GetComponent<SpriteRenderer>().color = lerpedColor;
        pass = true;

        // Wait for another duration before switching scenes
        yield return new WaitForSeconds(2f); // Adjust the duration as needed

        loadNextScene();
    }
}
