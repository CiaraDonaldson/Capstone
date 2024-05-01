using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fStoryMode : MonoBehaviour
{
    public GameObject Story;
    // Start is called before the first frame update
    void Start()
    {
        Story = GameObject.Find("StoryMode");
    }

    // Update is called once per frame
    void Update()
    {
        int y = SceneManager.GetActiveScene().buildIndex;
        if (Story != null && y <= 14)
        {
            this.gameObject.GetComponent<Sneak>().enabled = false;

        }
        if (Story != null && y > 14)
        {
            this.gameObject.GetComponent<Sneak>().enabled = true;
        }
         if (Story != null && y < 21)
        {
            this.gameObject.GetComponent<Taunt>().enabled = false;
        }
        if (Story != null && y >= 21)
        {
            this.gameObject.GetComponent<Taunt>().enabled = true;
        }

        if (Story == null)
        {
            this.gameObject.GetComponent<Taunt>().enabled = true;
            this.gameObject.GetComponent<Sneak>().enabled = true;
        }
    }
}
