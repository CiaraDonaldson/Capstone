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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fText.text = "Fox: " + fOrbs;
        hText.text = "Hawk: " + hOrbs;
        fText2.text = "Fox: " + fOrbs;
        hText2.text = "Hawk: " + hOrbs;

    }

    public void startGame()
    {
        SceneManager.LoadScene("Mechanic Test");
    }
    public void startCredit()
    {
        SceneManager.LoadScene("Credits");
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
