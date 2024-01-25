using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChimeraMovement : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim.Play("Sit");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
