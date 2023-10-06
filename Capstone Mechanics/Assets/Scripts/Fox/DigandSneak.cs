using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigandSneak : MonoBehaviour
{
    public float radius = 2f;
    public float digDepth = 1f;
    public LayerMask digLayer;
    public bool isDigging = false;
    public CircleCollider2D cCollider;
    public CapsuleCollider2D capCollider;
    public Animator anim;
    public Sprite thisSprite;

    public SpriteRenderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        cCollider = GetComponent<CircleCollider2D>();
        capCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 raycastOrigin = transform.position;
        Vector3 raycastDirection = transform.forward;

        if (Input.GetKey(KeyCode.DownArrow))
        {
            isDigging = true;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, digDepth, digLayer);
            Debug.DrawRay(raycastOrigin, raycastDirection * 2, Color.green);
            if (!hit)
            {
                cCollider.enabled = false;
                capCollider.enabled = true;
                rend.sprite = thisSprite;
            }
           
            if (hit.collider != null)
            {
                anim.Play("Dig");
                Destroy(hit.collider.gameObject);

            }
        }
        else
        {
            isDigging = false;
            cCollider.enabled = true;
            capCollider.enabled = false;

        }
    }
}
