using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayonTarget : MonoBehaviour
{
    public GameObject track;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = track.transform.position;
    }
}
