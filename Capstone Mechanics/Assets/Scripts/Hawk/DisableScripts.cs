using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableScripts : MonoBehaviour
{
    public FoxController ctrl;
    public Dig d;
    public Taunt t;
    public Sneak s;
    // Start is called before the first frame update
    void Start()
    {
        DisableFoxComponents();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DisableFoxComponents()
    {
        if (ctrl != null)
        {
            ctrl.enabled = false;
            Debug.Log("FoxController component found on Fox.");
        }
        else
            Debug.LogError("FoxController component not found on Fox.");

        if (t != null)
        {
            t.enabled = false;
            Debug.Log("Taunt component found on Fox.");
        }
        else
            Debug.LogError("Taunt component not found on Fox.");

        if (d != null)
        {
           d.enabled = false;
            Debug.Log("Dig component found on Fox.");
        }
        else
            Debug.LogError("Dig component not found on Fox.");

        if (s != null)
        {
            s.enabled = false;
            Debug.Log("Sneak component found on Fox.");
        }

        else
            Debug.LogError("Sneak component not found on Fox.");
    }

    public void EnableFoxComponents()
    {
        this.GetComponent<FoxController>().enabled = true;
        this.GetComponent<Taunt>().enabled = true;
        this.GetComponent<Dig>().enabled = true;
        this.GetComponent<Sneak>().enabled = true;
    }
}
