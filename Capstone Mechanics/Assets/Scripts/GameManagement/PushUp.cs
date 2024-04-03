using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushUp : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Fox" || collision.gameObject.name == "Hawk")
        {
            collision.transform.Translate(Vector3.up * 3f);

        }
    }
}
