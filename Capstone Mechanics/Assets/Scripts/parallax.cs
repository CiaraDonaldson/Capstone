using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class parallax : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] bool scrollLeft;

    float singleTextureWidth;
    public CinemachineVirtualCamera virtualCamera;
    // Start is called before the first frame update
    void Start()
    {
        SetupTexture();
        if (GameObject.Find("Main Camera"))
        {
            virtualCamera = GameObject.Find("Main Camera").GetComponent<CinemachineVirtualCamera>();
        }
        else
        {
            this.GetComponent<parallax>().enabled = false;
        }

    }

    void SetupTexture()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        singleTextureWidth = sprite.texture.width / sprite.pixelsPerUnit;
    }

    void Scroll()
    {
        float delta = moveSpeed * Time.deltaTime;

        // Move the object
        // transform.position += new Vector3(delta, 0f, 0f);
        transform.position += new Vector3(virtualCamera.transform.position.x - transform.position.x, 0f, 0f);


        // Move the Cinemachine camera in the opposite direction when scrollLeft is true
        if (scrollLeft)
        {
            transform.position -= new Vector3(delta, 0f, 0f);
        }
        else
        {
            transform.position += new Vector3(delta, 0f, 0f);
        }
        /*float delta = moveSpeed + Time.deltaTime;
        transform.position += new Vector3(delta, 0f, 0f);

        if (scrollLeft)
        {
            transform.position -= new Vector3(delta * 2f, 0f, 0f); // Adjust the factor as needed
        }*/
    }

    void CheckReset()
    {
        if ((Mathf.Abs(transform.position.x) - singleTextureWidth) > 0)
        {
            transform.position = new Vector3(0.0f, transform.position.y, transform.position.z);
        }
    }

    void Update()
    {
        Scroll();
        CheckReset();
     
    }
}
