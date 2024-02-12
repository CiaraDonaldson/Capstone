using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleAudio : MonoBehaviour
{
    private void Awake()
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.buildIndex == 0 || scene.buildIndex == 1 || scene.buildIndex == 2 || scene.buildIndex == 3)
        {
            DontDestroyOnLoad(GameObject.Find("TitleAudio"));
        }
        
        
    
        //string SName = scene.name.ToString();
        //if (GameObject.Find("Title Audio") != this.gameObject)
        //{
       //     Destroy(this.gameObject);
      //  }

    }
    void Update()
    {

      
    }
}
