using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colorlessinteract : MonoBehaviour
{
    public string popUp;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Hawk")
        {
            PopUpSystem pop = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PopUpSystem>();
            pop.PopUp(popUp);
        }
        else if (other.gameObject.name == "Fox")
        {
            PopUpSystem pop = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PopUpSystem>();
            pop.PopUp(popUp);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PopUpSystem pop = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PopUpSystem>();
            pop.PopDown();
        }
    }
 }
