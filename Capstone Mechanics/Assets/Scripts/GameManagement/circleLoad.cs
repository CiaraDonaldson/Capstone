using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class circleLoad : MonoBehaviour
{
    [Header("Timers")]
    [SerializeField] private float indicatorTimer = 1.0f;
    [SerializeField] private float maxIndicatorTimer = 1.0f;

    [Header("Indicator")]
    [SerializeField] private Image radialIndicatorUI = null;
    [SerializeField] public GameObject radialIndicatorGO = null;

    [Header("Key Code")]
    [SerializeField] private KeyCode selectKey = KeyCode.N;

    private bool shouldUpdate = false;

    private void Start()
    {
        Transform canvasTransform = GameObject.Find("Canvas").transform;

        if (canvasTransform != null)
        {
            radialIndicatorGO = canvasTransform.Find("Image").gameObject;

            if (radialIndicatorGO != null)
            {
                radialIndicatorUI = radialIndicatorGO.GetComponent<Image>();
            }
        }
        if (radialIndicatorUI == null)
        {
            Debug.Log("Not Here");
        }
        radialIndicatorUI.enabled = false;
    }
    private void Update()
    {
        
        if (Input.GetKey(selectKey))
        {
            indicatorTimer -= Time.deltaTime/5;
            radialIndicatorUI.enabled = true;
            radialIndicatorUI.fillAmount = indicatorTimer;

            if (indicatorTimer <= 0)
            {
                indicatorTimer = maxIndicatorTimer;
                radialIndicatorUI.fillAmount = maxIndicatorTimer;
                radialIndicatorUI.enabled = false;
            }
        }
        else
        {
            if (shouldUpdate)
            {
                indicatorTimer += Time.deltaTime/5;
                radialIndicatorUI.fillAmount = indicatorTimer;
                if (indicatorTimer >= maxIndicatorTimer)
                {
                    indicatorTimer = maxIndicatorTimer;
                    radialIndicatorUI.fillAmount = maxIndicatorTimer;
                    radialIndicatorUI.enabled = false;
                    shouldUpdate = false;
                }
            }

        }

        if (Input.GetKeyUp(selectKey))
        {
            shouldUpdate = true;
        }
    }
}
