using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public bool optionsOpen = false;
    public GameObject pauseMenuUI;
    public GameObject Hawk;
    public GameObject Fox;
    public GameObject optionMenuUI;

    private void Awake()
    {
    }

    private void Start()
    {
        Fox = GameObject.Find("Fox");
        Hawk = GameObject.Find("Hawk");
        //pauseMenuUI = GameObject.Find("pauseMenuUI");
        pauseMenuUI.SetActive(false);
        optionMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    private void Update()
    {
        // Fox = GameObject.Find("Fox");
        //Hawk = GameObject.Find("Hawk");

        //s pauseMenuUI.SetActive(false);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();

                //optionMenuUI.SetActive(false);
            }
        }
        if (GameIsPaused)
        {
            if (optionsOpen)
            {
                Options(true);
            }
            else if (!optionsOpen)
            {
                Options(false);
            }
        }
        
    }

    private void Options(bool optionsOpen)
    {
        pauseMenuUI.SetActive(!optionsOpen);
        optionMenuUI.SetActive(optionsOpen);
    }
    private void pauseDone(bool optionsOpen)
    {
        pauseMenuUI.SetActive(optionsOpen);
        optionMenuUI.SetActive(optionsOpen);
    }

    public void Resume()
    {
        pauseDone(optionsOpen);
        Time.timeScale = 1f;
        GameIsPaused = false;
        
            TriggerEnabled(true);
        
    }

    private void Pause()
    {
        Cursor.visible = true;
        pauseMenuUI.SetActive(true);
        Options(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
        
            TriggerEnabled(false);
        
    }

    private void TriggerEnabled(bool enable)
    {
        Fox.GetComponent<Taunt>().enabled = enable;
        Fox.GetComponent<Dig>().enabled = enable;
        Fox.GetComponent<Sneak>().enabled = enable;
        Fox.GetComponent<FoxController>().enabled = enable;
        Hawk.GetComponent<HawkController>().enabled = enable;
        Hawk.GetComponent<Fly>().enabled = enable;
        Hawk.GetComponent<Scan>().enabled = enable;
        Hawk.GetComponent<Carry>().enabled = enable;
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