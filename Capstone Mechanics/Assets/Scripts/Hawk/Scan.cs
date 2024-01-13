using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public class Scan : MonoBehaviour
{
    public float elevateHeight = 5f;
    public float cameraZoomOutSize = 30f;
    public float orbsMoveDistance = -5f;
    public float lerpSpeed = 5f;
    public float keyHoldDuration = 5f;
    public CinemachineVirtualCamera virtualCamera;
    public GameObject Fox;
    public float wKeyPressStartTime = 0f;
    public List<Transform> orbs = new List<Transform>();
    public bool canScan = true;
    public GameObject Smoke;

    private bool isElevating = false;
    private bool isWKeyPressed = false;
    private Vector3 originalPlayerPosition;
    private float originalCameraSize;
    private float originalZPosition;
    private bool isZoomedOut = false;
    private Animator anim;
    private bool orbsMoved = false;

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
        originalCameraSize = Camera.main.orthographicSize;
    }

    private void Update()
    {
        RemoveNullTransformsFromList();
        if (Input.GetKey(KeyCode.W))
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
            }
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            if (isWKeyPressed)
            {
                EndElevate();
            }

            isWKeyPressed = false;
            wKeyPressStartTime = 0f;
            //anim.Play("Idle");
        }

        if (isElevating)
        {
            anim.Play("Scan");
            // Lerping the player position
            float lerpProgress = Mathf.Clamp01(Time.time * lerpSpeed);
            Vector3 targetPosition = originalPlayerPosition + Vector3.up * elevateHeight;
            transform.position = Vector3.Lerp(originalPlayerPosition, targetPosition, lerpProgress);

            // Zooming out the camera
            isZoomedOut = true;

            // Set the new orthographic size (or FOV)
            virtualCamera.m_Lens.OrthographicSize = cameraZoomOutSize;

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
        isElevating = true;
        isWKeyPressed = true;
        originalPlayerPosition = transform.position;
    }

    private void EndElevate()
    {
        isElevating = false;
        orbsMoved = false;
        // Reset player position
        transform.position = originalPlayerPosition;

        if (isZoomedOut)
        {
            isZoomedOut = false;
            // Reset to the original orthographic size (or FOV)
            virtualCamera.m_Lens.OrthographicSize = originalCameraSize;
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