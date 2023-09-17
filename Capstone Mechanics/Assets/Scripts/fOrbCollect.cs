using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fOrbCollect : MonoBehaviour
{
    public GameManager fManager;
    // Start is called before the first frame update
    void Start()
    {
        GameObject gManager = GameObject.Find("GameManager");
        fManager = gManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Orb") && collision.gameObject.name == "fOrb")
        {
            fManager.addFOrb();
            Destroy(collision.gameObject);
            
        }
    }
}
