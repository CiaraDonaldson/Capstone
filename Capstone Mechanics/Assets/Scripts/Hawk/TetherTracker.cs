using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetherTracker : MonoBehaviour
{
    public GameObject Hawk;
    public Vector2 savePos;
    private bool keyIsPressed = false;
    private float keyPressStartTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Hawk = GameObject.Find("Hawk");
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.W))
        {
            savePos = Hawk.transform.position;

            keyIsPressed = true;
            keyPressStartTime += Time.deltaTime;
            if (keyPressStartTime >= 5f)
            {
                this.transform.position = savePos;
            }
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            keyPressStartTime = 0f;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.position = Hawk.transform.position;
        }
    }
}
