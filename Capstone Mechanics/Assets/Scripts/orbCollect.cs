using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orbCollect : MonoBehaviour
{
    public GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        GameObject gManager = GameObject.Find("GameManager");
        manager = gManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Orb"))
        {
            manager.addOrb();
            Destroy(collision.gameObject);
            
        }
    }
}
