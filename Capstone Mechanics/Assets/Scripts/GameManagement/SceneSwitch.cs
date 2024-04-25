using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public float nKeyPressStartTime = 5;

    // Start is called before the first frame update
    private void Update()
    {
        if (Input.GetKey(KeyCode.N))
        {
            nKeyPressStartTime -= Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.N))
        {
            nKeyPressStartTime += Time.deltaTime;
        }
        if (nKeyPressStartTime <= 0f)
        {
            Debug.Log("it hit 5");
            if (SceneManager.GetActiveScene().name.ToString() == "LivingWorld")
            {
                SceneManager.LoadScene("TheFall");
            }
            if (SceneManager.GetActiveScene().name.ToString() == "Lvl1 Cutscene")
            {
                SceneManager.LoadScene("Lvl1");
            }
            if (SceneManager.GetActiveScene().name.ToString() == "Lvl3Cutscene")
            {
                SceneManager.LoadScene("Lvl3");
            }
            if (SceneManager.GetActiveScene().name.ToString() == "Lvl5Cutscene")
            {
                SceneManager.LoadScene("Lvl5");
            }
            if (SceneManager.GetActiveScene().name.ToString() == "Lvl7Cutscene")
            {
                SceneManager.LoadScene("Lvl7");
            }
        }
    }

    public void Title2()
    {
        SceneManager.LoadScene("TitleTwo");
    }

    public void Lvl1Cutscene()
    {
        SceneManager.LoadScene("Lvl1 Cutscene");
    }

    public void Lvl3Cutscene()
    {
        SceneManager.LoadScene("Lvl3Cutscene");
    }

    public void Lvl5Cutscene()
    {
        SceneManager.LoadScene("Lvl5Cutscene");
    }

    public void Lvl7Cutscene()
    {
        SceneManager.LoadScene("Lvl7Cutscene");
    }

    public void Lvl1()
    {
        SceneManager.LoadScene("Lvl1");
    }

    public void Lvl2()
    {
        SceneManager.LoadScene("Lvl2");
    }

    public void Lvl3()
    {
        SceneManager.LoadScene("Lvl3");
    }

    public void Lvl4()
    {
        SceneManager.LoadScene("Lvl4");
    }

    public void Lvl5()
    {
        SceneManager.LoadScene("Lvl5");
    }

    public void Lvl6()
    {
        SceneManager.LoadScene("Lvl6");
    }

    public void Lvl7()
    {
        SceneManager.LoadScene("Lvl7");
    }

    public void Lvl8()
    {
        SceneManager.LoadScene("Lvl8");
    }

    public void Lvl9()
    {
        SceneManager.LoadScene("Lvl9");
    }

    public void Lvl10()
    {
        SceneManager.LoadScene("Lvl10");
    }

    public void TheFall()
    {
        SceneManager.LoadScene("TheFall");
    }

    public void LiveEnd()
    {
        SceneManager.LoadScene("LivingWorldEnd");
    }
}