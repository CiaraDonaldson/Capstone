using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject childText; //  or make public and drag
    void Start()
    {
        //TextMeshPro text = GetComponentInChildren<Text>();
        if (childText != null)
        {
            //childText = text.gameObject;
            childText.SetActive(false);
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        childText.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        childText.SetActive(false);
    }
}
