using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hOrbCollect : MonoBehaviour
{
    public GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        GameObject gManager = GameObject.Find("GameManager");
        manager = gManager.GetComponent<GameManager>();
    }

 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Orb") && collision.gameObject.name == "hOrb")
        {
            manager.addHOrb();
            Destroy(collision.gameObject);
            this.GetComponent<Scan>().RemoveNullTransformsFromList();
        }
    }
}
