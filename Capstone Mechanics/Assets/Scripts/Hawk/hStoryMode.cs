using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class hStoryMode : MonoBehaviour
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
        if (Story != null && y <= 13)
        {
            this.gameObject.GetComponent<Scan>().enabled = false;
            this.gameObject.GetComponent<Carry>().enabled = false;

        }
        else if (Story != null && y == 15 || y == 16)
        {
            this.gameObject.GetComponent<Scan>().enabled = true;
            this.gameObject.GetComponent<Carry>().enabled = false;
        }
        else if (Story != null && y < 16)
        {
            this.gameObject.GetComponent<Scan>().enabled = true;
            this.gameObject.GetComponent<Carry>().enabled = true;
        }

        if (Story == null)
        {
            this.gameObject.GetComponent<Scan>().enabled = true;
            this.gameObject.GetComponent<Carry>().enabled = true;
        }
    }
}
