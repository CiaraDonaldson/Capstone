using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class Scan : MonoBehaviour
{
    public GameObject Hawk;
    public GameObject Fox;
    public float zoomOutSize = 5f;       
    public Rigidbody2D rb;
    public Rigidbody2D frb;
    public float requiredDuration = 5f;      
    public Transform chain;
    public CinemachineVirtualCamera virtualCamera;
    public List<Transform> orbs;

    private float originalZPosition;
    private bool keyIsPressed = false;
    public float keyPressStartTime = 0f;
    private Camera mainCamera;
    private Vector3 hawkOriginalPosition;
    private float cameraOriginalSize = 15f;
    private bool isZoomedOut = false;
    void Start()
    {        
        mainCamera = Camera.main;

       
        rb = GetComponent<Rigidbody2D>();
        Fox = GameObject.Find("Fox");
        frb = GameObject.Find("Fox").GetComponent<Rigidbody2D>();


        GameObject[] orbGameObjects = GameObject.FindGameObjectsWithTag("Orb");

        foreach (GameObject orbObject in orbGameObjects)
        {
            orbs.Add(orbObject.transform);
        }

        originalZPosition = orbs[0].position.z;
    }

    void Update()
    {
        
        if (Input.GetKey(KeyCode.W))
        {
            keyIsPressed = true;
            keyPressStartTime += Time.deltaTime;

            // Zoom Camera out
            if (keyPressStartTime == 5f && !isZoomedOut)
            {
                isZoomedOut = true;
                // Set the new orthographic size (or FOV)
                virtualCamera.m_Lens.OrthographicSize = zoomOutSize;


            }
            else if (keyPressStartTime >= 5f)
            {
                //rb.gravityScale = 0;
                frb.isKinematic = true;

                // Move the character up.
                transform.position = Vector3.up * 3;

                //check if position is correct to continue
                if (orbs[0].transform.position.z > -5f)
                {
                    //Change position of orbs
                    foreach (Transform orb in orbs)
                    {
                        //Vector3 newPosition = orb.position + -Vector3.forward * 5f;
                        orb.position += -Vector3.forward * 6f;
                        //Color check
                        //SpriteRenderer renderer = orb.GetComponent<SpriteRenderer>();
                        //renderer.color = Random.ColorHSV();
                    }
                }
                // Change the layer of orbs to make them visible.
                // ChangeLayerForOrbs(true);
            }
            else 
            {
                hawkOriginalPosition = Hawk.transform.position;
            }
        }
       else if (Input.GetKeyUp(KeyCode.W))
        {
           // rb.gravityScale = 1;
            frb.isKinematic = false;
            keyIsPressed = false;
            keyPressStartTime = 0f;

            //set position
            Hawk.transform.position = chain.position;

            //Reset Camera
            if (isZoomedOut)
            {
                isZoomedOut = false;
                // Reset to the original orthographic size (or FOV)
                virtualCamera.m_Lens.OrthographicSize = cameraOriginalSize;
            }

            foreach (Transform orb in orbs)
            {
                    Vector3 newPosition = orb.position;
                    newPosition.z = originalZPosition;
                    orb.position = newPosition;

                if (orb.name == "fOrb")
                {
                    SpriteRenderer renderer = orb.GetComponent<SpriteRenderer>();
                    //renderer.color = Color.blue;
                }
                if (orb.name == "hOrb")
                {
                    SpriteRenderer renderer = orb.GetComponent<SpriteRenderer>();
                    //renderer.color = Color.red;
                }

            }

            // Revert the layer change for orbs.
            //ChangeLayerForOrbs(false);

        }
    }

    public void RemoveNullTransformsFromList()
    {
        // Iterate through the list in reverse to safely remove elements.
        for (int i = orbs.Count - 1; i >= 0; i--)
        {
            // Check if the transform in the list is null.
            if (orbs[i] == null)
            {
                // Remove the null transform from the list.
                orbs.RemoveAt(i);
            }
        }
    }
  
}
