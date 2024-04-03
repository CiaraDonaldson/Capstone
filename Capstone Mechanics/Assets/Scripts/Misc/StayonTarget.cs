using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayonTarget : MonoBehaviour
{
    public Transform startObject;
    public Transform endObject;
    public LineRenderer lineRenderer;
    public int numberOfPoints = 5;  // Adjust the number of points as needed
    public float gravity = 9.8f;
    public float maxDistanceForCurve = 110f;
    public float maxAlpha = 1.0f;  // Maximum alpha (fully opaque)
    public float minAlpha = 0.3f;  // Minimum alpha (partially transparent)

    void Start()
    {
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }
    }

    void Update()
    {
        if (startObject != null && endObject != null)
        {
            SetLinePositions();
            SetLineTransparency();
        }
        else
        {
            Debug.LogError("Please assign start and end objects.");
        }
    }

    void SetLinePositions()
    {
        float distance = Vector3.Distance(startObject.position, endObject.position);

        if (distance >= maxDistanceForCurve)
        {
            // If the distance is 10 or greater, make the line completely straight
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, startObject.position);
            lineRenderer.SetPosition(1, endObject.position);
        }
        else
        {
            // Otherwise, create a U curve with multiple points affected by gravity
            lineRenderer.positionCount = numberOfPoints + 2;

            lineRenderer.SetPosition(0, startObject.position);

            // Calculate the step size between each point
            float stepSize = 1.0f / (numberOfPoints + 1);

            // Update the positions of the middle points affected by gravity
            for (int i = 1; i <= numberOfPoints; i++)
            {
                float t = i * stepSize;
                Vector3 pointPosition = Vector3.Lerp(startObject.position, endObject.position, t);
                pointPosition.y -= gravity * t * (1 - t);  // Apply gravity effect
                lineRenderer.SetPosition(i, pointPosition);
            }

            lineRenderer.SetPosition(numberOfPoints + 1, endObject.position);
        }
    }

    void SetLineTransparency()
    {
        float distance = Vector3.Distance(startObject.position, endObject.position);
        float alpha = Mathf.Lerp(maxAlpha, minAlpha, 1 - Mathf.InverseLerp(0, maxDistanceForCurve, distance));
        Color lineColor = lineRenderer.material.color;
        lineColor.a = alpha;
        lineRenderer.material.color = lineColor;
    }
}
