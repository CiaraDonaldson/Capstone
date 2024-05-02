using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChimeraMovement : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        int lvl = SceneManager.GetActiveScene().buildIndex;
        if (lvl == 18 || lvl == 22 || lvl == 24)
        { 
            anim.Play("Down");
        }
        else
        {
            anim.Play("Sit");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
