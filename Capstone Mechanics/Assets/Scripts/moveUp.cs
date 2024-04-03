using UnityEngine;
using TMPro;

public class moveUp : MonoBehaviour
{
    public float scrollSpeed = 1.0f;

    private RectTransform rectTransform;
    private float textHeight;
    private Vector2 initialPosition;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        // Calculate the height of the text
        TMP_Text textComponent = GetComponent<TMP_Text>();
        textHeight = textComponent.preferredHeight;

        // Store the initial position of the text
        initialPosition = rectTransform.anchoredPosition;
    }

    void Update()
    {
        // Move the text upwards
        rectTransform.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

        // If the text has moved past its height, reset its position
        if (rectTransform.anchoredPosition.y - initialPosition.y >= textHeight)
        {
            // Reset the text position to the initial position
            rectTransform.anchoredPosition = initialPosition;
        }
    }
}
