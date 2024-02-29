using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FAudio : MonoBehaviour
{
    public float digDepth = 3f;
    public LayerMask digLayer;

    public AudioClip sneak;
    public AudioClip run;
    public AudioClip taunt;
    public AudioClip dig;
    public AudioSource MyAudioSource;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow)) || (Input.GetKeyDown(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow)))
        {
            MyAudioSource.clip = sneak;
            MyAudioSource.Play();
        }
        else if(!Input.GetKey(KeyCode.DownArrow) && Input.GetKeyUp(KeyCode.LeftArrow) | Input.GetKeyUp(KeyCode.RightArrow))
        {
            MyAudioSource.Stop();
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, digDepth, digLayer);

        if (hit.collider != null)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                MyAudioSource.clip = dig;
                MyAudioSource.Play();
            }
        }
        else if(Input.GetKeyUp(KeyCode.DownArrow))
        {
            MyAudioSource.Stop();
        }

        if ((!Input.GetKey(KeyCode.DownArrow) && Input.GetKeyDown(KeyCode.LeftArrow)) | (!Input.GetKey(KeyCode.DownArrow) && Input.GetKeyDown(KeyCode.RightArrow)))
        {
            MyAudioSource.clip = run;
            MyAudioSource.Play();
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            StartCoroutine(audioStop());
        }
    }

    public IEnumerator audioStop()
    {
        yield return new WaitForSeconds(1f);
        MyAudioSource.Stop();
    }
}

