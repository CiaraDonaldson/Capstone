using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class TimelineTrigger : MonoBehaviour
{
    public GameObject cameraHold;
    public GameObject cameraCur;
    public GameObject dialougeBox;
    public PlayableDirector timeline;
    public GameObject GameManager;
    public int fOrbCount = 10;
    public int hOrbCount = 10;
    public int count = 0;

    private void Awake()
    {
        timeline.Stop();
        cameraHold.SetActive(false);
        cameraCur.SetActive(true);
        dialougeBox.SetActive(false);

    }

    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().fOrbs == fOrbCount && GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().hOrbs == hOrbCount)
        {
            count++;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Hawk" || collision.gameObject.tag == "Fox" && count >= 1)
        {
            timeline.Play();
            this.GetComponent<BoxCollider2D>().enabled = false;
            cameraHold.SetActive(true);
            cameraCur.SetActive(false);
            dialougeBox.SetActive(true);

        }
    }
}