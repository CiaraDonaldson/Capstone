using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject Hawk;
    public GameObject Fox;
    private void Start()
    {
        Fox = GameObject.Find("Fox");
        Hawk = GameObject.Find("Hawk");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else 
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Fox.GetComponent<Taunt>().enabled = true;
        Fox.GetComponent<DigandSneak>().enabled = true;
        Fox.GetComponent<FoxController>().enabled = true;
        Hawk.GetComponent<HawkController>().enabled = true;
        Hawk.GetComponent<Fly>().enabled = true;
        Hawk.GetComponent<Scan>().enabled = true;
        Hawk.GetComponent<Carry>().enabled = true;
    }
    void Pause()
    {
        Cursor.visible = true;

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Fox.GetComponent<Taunt>().enabled = false;
        Fox.GetComponent<DigandSneak>().enabled = false;
        Fox.GetComponent<FoxController>().enabled = false;
        Hawk.GetComponent<HawkController>().enabled = false;
        Hawk.GetComponent<Fly>().enabled = false;
        Hawk.GetComponent<Scan>().enabled = false;
        Hawk.GetComponent<Carry>().enabled = false;

    }
    public void LoadMenu()
    {
       // Cursor.visible = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Title");
              
    }

    public void LoadMap()
    {
        //Cursor.visible = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Map");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game....");
        Application.Quit();
    }
}
