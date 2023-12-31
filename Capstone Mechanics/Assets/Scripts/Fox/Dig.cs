using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dig : MonoBehaviour
{
    public float digDepth = 3f;  // Depth to dig
    public LayerMask digLayer;   // Layer to check for digging
    private bool isDigging = false;
    public Animator anim;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && !isDigging)
        {
            StartCoroutine(DigCoroutine());
        }

    }

    IEnumerator DigCoroutine()
    {
        isDigging = true;
        // Perform raycast
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, digDepth, digLayer);

        if (hit.collider != null)
        {
            anim.Play("Dig");
            yield return new WaitForSeconds(.5f);
            // Object on the dig layer is hit, destroy it
            Destroy(hit.collider.gameObject);
        }
        
        isDigging = false;
    }
}
