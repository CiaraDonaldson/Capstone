using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colorlessinteract : MonoBehaviour
{
    public string popUp;
    public int fOrbCount = 3;
    public int hOrbCount = 2;
    public bool pass = false;
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
        if (GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().fOrbs == fOrbCount && GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().hOrbs == hOrbCount)
        {
            PopUpSystem pop = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PopUpSystem>();
            pop.PopUp("You Did It! Thank you!");
            Color lerpedColor = Color.Lerp(Color.white, Color.magenta, 20f);
            this.GetComponent<SpriteRenderer>().color = lerpedColor;
            pass = true;
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
