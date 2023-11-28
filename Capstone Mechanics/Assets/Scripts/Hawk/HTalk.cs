using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HTalk : MonoBehaviour
{
    public GameObject popUpBox;
    public Animator animator;
    public TMP_Text popUpText;
    public string popUptext;

    private GameObject Hawk;
    private GameObject Fox;
    private Vector3 initialScale;
    private Quaternion initialRotation;
    // Start is called before the first frame update
    void Start()
    {
        initialScale = transform.localScale;
        initialRotation = transform.rotation;
        Hawk = GameObject.Find("Hawk");
        Fox = GameObject.Find("Fox");
    }

    // Update is called once per frame
    void Update()
    {

        if (!Hawk.GetComponent<DigandSneak>().inAir)
        {
            StartCoroutine(canScanNeg());
        }
        if (Hawk.GetComponent<DigandSneak>().inAir)
        {
            PopDown();
        }
    }
    /*void LateUpdate()
    {
        // Keep the scale and rotation constant in LateUpdate
        transform.localScale = initialScale;
        transform.rotation = initialRotation;
    }*/
    IEnumerator canScanNeg()
        {
            PopUp("Stupid Bird! Can you touch the ground!?");
            yield return new WaitForSeconds(5f);
            //PopDown();
        }
      public void PopUp(string text)
    {
        popUpBox.SetActive(true);
        popUpText.text = text;
        animator.Play("pop");
    }
    public void PopDown()
    {
        animator.Play("close");
        //popUpBox.SetActive(false);
    }
}

