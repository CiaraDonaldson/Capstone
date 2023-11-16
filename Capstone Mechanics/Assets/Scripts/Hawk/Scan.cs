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
    public float keyPressStartTime = 0f;
    private Camera mainCamera;
    private Vector3 hawkOriginalPosition;
    private float cameraOriginalSize = 15f;
    private bool isZoomedOut = false;
    private Animator anim;
    public float lerpSpeed = 10.0f;
    private Renderer rend;


    private float colorLerpDuration = 2.0f; // Duration for color interpolation.
    private float colorLerpTimer = 0.0f;     // Timer for color interpolation.

    private Color startColor = Color.white;  // Initial color.
    private Color targetColor = Color.red;   // Target color.
    private bool isLerpingColor = false;     // Flag to control color lerp.

    void Start()
    {        
        mainCamera = Camera.main;

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Fox = GameObject.Find("Fox");
        frb = GameObject.Find("Fox").GetComponent<Rigidbody2D>();
        rend = GetComponent<Renderer>();

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
            keyPressStartTime += Time.deltaTime;
            RemoveNullTransformsFromList();

            // Zoom Camera out
            if (keyPressStartTime == 5f && !isZoomedOut)
            {
                isZoomedOut = true;

                // Set the new orthographic size (or FOV)
                virtualCamera.m_Lens.OrthographicSize = zoomOutSize;


            }
            else if (keyPressStartTime >= 5f && Fox.GetComponent<Rigidbody2D>().velocity.x == 0 && Fox.GetComponent<Rigidbody2D>().velocity.y == 0)
            {
                isLerpingColor = true;
                colorLerpTimer = 0.0f;

                //rb.gravityScale = 0;
                frb.isKinematic = true;

                // Move the character up.
                transform.position = Vector3.up * 3;

                anim.Play("Scan");
                //check if position is correct to continue
                if (orbs[0].transform.position.z > -5f)
                {
                    //Change position of orbs
                    foreach (Transform orb in orbs)
                    {
                        
                        orb.position += -Vector3.forward * 6f;
                      
                    }
                }
               
            }
            else 
            {
                hawkOriginalPosition = Hawk.transform.position;
            }



            if (isLerpingColor)
            {
                // Increment the color lerp timer.
                colorLerpTimer += Time.deltaTime;

                // Calculate the lerp progress.
                float lerpProgress = colorLerpTimer / colorLerpDuration;

                // Perform color lerp between startColor and targetColor.
                Color lerpedColor = Color.Lerp(startColor, targetColor, lerpProgress);

                // Apply the lerped color to the object's renderer.
                rend.material.color = lerpedColor;

                // Check if color lerp is complete.
                if (lerpProgress >= 1.0f)
                {
                    isLerpingColor = false;
                    colorLerpTimer = 0.0f;
                }
            }
        }
       else if (Input.GetKeyUp(KeyCode.W))
        {
            this.GetComponent<Renderer>().material.color = Color.white;
            // rb.gravityScale = 1;
            frb.isKinematic = false;
            keyPressStartTime = 0f;

            //set position
            Hawk.transform.position = chain.position;
            if (keyPressStartTime > 5)
            {
                anim.Play("Idle");
            }
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
