using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HAudio : MonoBehaviour
{
  

    public AudioClip fly;
    public AudioClip scan;
    public AudioSource MyAudioSource;
    private float wKeyPressStartTime;
    private float keyHoldDuration = 5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            MyAudioSource.clip = fly;
            MyAudioSource.pitch = Random.Range(1f, 1.5f);
            MyAudioSource.Play();
        }
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            MyAudioSource.Stop();
        }

        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && Input.GetKeyDown(KeyCode.W))
        {
            MyAudioSource.clip = fly;
            MyAudioSource.pitch = Random.Range(1.4f, 2f);
            MyAudioSource.Play();
        }
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            MyAudioSource.Stop();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            MyAudioSource.clip = fly;
            MyAudioSource.pitch = Random.Range(1.4f, 2f);
            MyAudioSource.Play();
            wKeyPressStartTime = 0f;
        }
        else if (Input.GetKeyUp(KeyCode.W))
        { 
            MyAudioSource.Stop();
            
        }
        if (Input.GetKey(KeyCode.W))
        {
            wKeyPressStartTime += Time.deltaTime;

            if (wKeyPressStartTime >= keyHoldDuration)
            {
                MyAudioSource.clip = scan;
                MyAudioSource.Play();
                MyAudioSource.loop = false;
            }

        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            wKeyPressStartTime = 0f;
            audioStop();
            MyAudioSource.loop = true;
        }
    }

    public IEnumerator audioStop()
    {
        yield return new WaitForSeconds(1f);
        MyAudioSource.Stop();
    }
}

