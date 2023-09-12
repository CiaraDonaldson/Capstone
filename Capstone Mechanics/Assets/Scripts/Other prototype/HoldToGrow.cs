using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldToGrow : MonoBehaviour
{
    public KeyCode actionButton;
    public float growSpeed = 1f;
    public float radius = 2f;
    public Color startColor = Color.white;
    public Color endColor = Color.red;
    public float colorChangeSpeed = .2f;

    private bool isHolding = false;
    private float t = 0f;
    void Update()
    {
        if (Input.GetKeyDown(actionButton))
        {
            isHolding = true;
        }

        if (Input.GetKeyUp(actionButton))
        {
            isHolding = false;
        }

        if (isHolding)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);

            foreach (Collider2D collider in colliders)
            {
                
                if (collider.gameObject.CompareTag("Scalable"))
                {
                    
                    Vector3 newScale = collider.transform.localScale + Vector3.one * growSpeed * Time.deltaTime;
                    collider.transform.localScale = newScale;

                   
                    SpriteRenderer spriteRenderer = collider.gameObject.GetComponent<SpriteRenderer>();
                    if (spriteRenderer != null)
                    {
                        spriteRenderer.color = Color.Lerp(startColor, endColor, t);
                    }
                }
            }

           
            t += colorChangeSpeed * Time.deltaTime;
            
            t = Mathf.Clamp01(t);
        }
        else
        {
           
            t = 0f;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}