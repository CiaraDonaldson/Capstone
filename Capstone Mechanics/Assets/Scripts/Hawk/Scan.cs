using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public class Scan : MonoBehaviour
{
    public float elevateHeight = 5f;
    public float cameraZoomOutSize = 9f;
    public float orbsMoveDistance = -5f;
    public float lerpSpeed = 5f;
    public float keyHoldDuration = 5f;
    public CinemachineVirtualCamera virtualCamera;
    public CinemachineComponentBase componentBase;
    public GameObject Fox;
    public float wKeyPressStartTime = 0f;
    public List<Transform> orbs = new List<Transform>();
    public bool canScan = true;
    public GameObject Smoke;

    public bool isElevating = false;
    private bool isWKeyPressed = false;
    private Vector3 originalPlayerPosition;
    private float originalCameraSize = 1;
    private float originalZPosition;
    private bool isZoomedOut = false;
    private Animator anim;
    private bool orbsMoved = false;

    public AudioSource MyAudioSource;
    public AudioClip scan;

    private void Start()
    {
        Fox = GameObject.Find("Fox");
        //Smoke = GameObject.Find("HawkSmoke");
        anim = GetComponent<Animator>();
        // Assuming orbs have the "Orb" tag. Adjust accordingly.
        GameObject[] orbGameObjects = GameObject.FindGameObjectsWithTag("Orb");

        foreach (GameObject orbObject in orbGameObjects)
        {
            orbs.Add(orbObject.transform);
        }
        originalZPosition = orbs[0].position.z;

        MyAudioSource = this.GetComponent<AudioSource>();
        
    }

    private void Update()
    {
        if (wKeyPressStartTime >= keyHoldDuration)
        {
            MyAudioSource.clip = scan;
            //MyAudioSource.loop = false;
            MyAudioSource.Play();
        }
        RemoveNullTransformsFromList();
        if (Input.GetKey(KeyCode.W))
        {           

            if (!this.GetComponent<Carry>().isCarrying)
            {
                wKeyPressStartTime += Time.deltaTime;
                if (Fox.GetComponent<Sneak>().inAir)
                {
                    canScan = false;
                }
                else
                {
                    canScan = true;
                }
                if (wKeyPressStartTime > 1 && wKeyPressStartTime < 5)
                {
                    Smoke.SetActive(true);
                }
                else
                {
                    Smoke.SetActive(false);
                }

                if (wKeyPressStartTime >= keyHoldDuration && !isElevating && !isZoomedOut && canScan)
                {
                    

                    StartElevate();

                   
                    if (Fox != null)
                    {
                        Fox.GetComponent<FoxController>().enabled = false;
                        Fox.GetComponent<Dig>().enabled = false;
                    }

                }
              
            }
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            
            if (isWKeyPressed)
            {
                EndElevate();
            }
            if (Fox != null)
            {
                Fox.GetComponent<FoxController>().enabled = true;
                Fox.GetComponent<Dig>().enabled = true;
                //EnableFoxComponents();
            }

            isWKeyPressed = false;
            wKeyPressStartTime = 0f;
            //anim.Play("Idle");
        }

        if (isElevating)
        {
            

            // Lerping the player position
            float lerpProgress = Mathf.Clamp01(Time.time * lerpSpeed);
            Vector3 targetPosition = originalPlayerPosition + Vector3.up * elevateHeight;
            transform.position = Vector3.Lerp(originalPlayerPosition, targetPosition, lerpProgress);

            // Zooming out the camera
            isZoomedOut = true;

            // Set the new orthographic size (or FOV)
            //virtualCamera.m_Lens.OrthographicSize = cameraZoomOutSize;
            if (componentBase == null)
            {
                componentBase = virtualCamera.GetCinemachineComponent(CinemachineCore.Stage.Body);
            }

            if (componentBase is CinemachineFramingTransposer)
            {
                (componentBase as CinemachineFramingTransposer).m_MinimumOrthoSize = cameraZoomOutSize;
            }

            if (!orbsMoved)
            {
                foreach (Transform orb in orbs)
                {
                    Vector3 orbTargetPosition = orb.position + Vector3.back * orbsMoveDistance;
                    orb.position = Vector3.Lerp(orb.position, orbTargetPosition, lerpProgress);
                }

                orbsMoved = true; // Set the flag to true to indicate that orbs have been moved
            }
        }
    }

    private void StartElevate()
    {
        anim.Play("Scan");
        isElevating = true;
        isWKeyPressed = true;
        originalPlayerPosition = transform.position;
        this.GetComponent<HawkController>().enabled = false;


    }

    private void EndElevate()
    {
        isElevating = false;
        orbsMoved = false;
        // Reset player position
        transform.position = originalPlayerPosition;

        this.GetComponent<HawkController>().enabled = true;


        if (isZoomedOut)
        {
            isZoomedOut = false;
            // Reset to the original orthographic size (or FOV)
            //virtualCamera.m_Lens.OrthographicSize = originalCameraSize;
            if (componentBase is CinemachineFramingTransposer)
            {
                (componentBase as CinemachineFramingTransposer).m_MinimumOrthoSize = originalCameraSize;
            }
        }
        // Reset orb positions
        foreach (Transform orb in orbs)
        {
            Vector3 orbPosition = orb.position;
            orbPosition.z = originalZPosition;
            orb.position = orbPosition;
        }
    }

    public void RemoveNullTransformsFromList()
    {
        orbs.RemoveAll(orb => orb == null);
    }


}

