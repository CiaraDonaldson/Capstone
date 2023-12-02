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


    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        fText.text = fOrbs.ToString();
        hText.text =  hOrbs.ToString();
        fText2.text = fOrbs.ToString();
        hText2.text =  hOrbs.ToString();

        string sceneName = SceneManager.GetActiveScene().ToString();

       /* if (sceneName != "Title" | !pauseMenuUI.enabled)
        {
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }*/
    }

    public void startGame()
    {
       // Cursor.visible = true;

        SceneManager.LoadScene("Mechanic Test");
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
}
