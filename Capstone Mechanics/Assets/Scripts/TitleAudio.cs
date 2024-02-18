using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleAudio : MonoBehaviour
{
    public AudioSource audioSource;
   // public GameObject titleAudio;
    private void Update()
    {
        //GameObject titleAudio = GameObject.FindWithTag("TitleAudio");
        DontDestroyOnLoad(transform.gameObject);
        audioSource = this.GetComponent<AudioSource>();
        Scene scene = SceneManager.GetActiveScene();

        // if (titleAudio == null)
        //   {
        //       Instantiate(titleAudio);
        //   }
        // Check if the scene is one of the specified scenes
        if (SceneManager.GetActiveScene().buildIndex >= 0 && SceneManager.GetActiveScene().buildIndex <= 4)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
                audioSource.volume = 1;
                audioSource.enabled = true;
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
                audioSource.volume = 0;
                audioSource.enabled = false;
            }
        }


    }

}
