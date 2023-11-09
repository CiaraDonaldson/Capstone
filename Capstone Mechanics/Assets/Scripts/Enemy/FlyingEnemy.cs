using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public int speed = 2;
    public bool go = false;
    public Transform startingPoint;
    public Transform endingPoint;
    public GameObject GameManager;
    private bool isFacingRight = true;
    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (go == true)
        {
            Go();
        }
        else
        {
            ReturnStartPosition();
        }

        if (this.transform.position == endingPoint.position)
        {
            FlipCharacter(false);
            go = false;
        }
        if (this.transform.position == startingPoint.position)
        {
            FlipCharacter(true);
            go = true;
        }
    }

    private void ReturnStartPosition()
    {
        transform.position = Vector2.MoveTowards(transform.position, startingPoint.position, speed * Time.deltaTime);
    }
    private void Go()
    {
        transform.position = Vector2.MoveTowards(transform.position, endingPoint.position, speed * Time.deltaTime);
    }

    void FlipCharacter(bool faceRight)
    {
        

        // Flip the character's scale based on the direction.
        Vector3 newScale = this.transform.localScale;
        newScale.x = faceRight ? 1 : -1;
        this.transform.localScale = newScale;

        isFacingRight = faceRight;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Hawk" || collision.gameObject.name == "Fox")
         {
            GameManager.GetComponent<PlayerHealth>().SetHealth(-10);
        }
    }

}
