using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorLerp : MonoBehaviour
{
    bool changingColor = false;

    public Color startColor;
    public Color endColor;

    // Start is called before the first frame update
    void Start()
    {
        //Color lilacColor = new Color(0.8f, 0.6f, 1.0f);
        LerpColor(this.GetComponent<MeshRenderer>(), startColor, endColor, 5f);
    }

    // Update is called once per frame
   

    IEnumerator LerpColor(MeshRenderer mesh, Color fromColor, Color toColor, float duration)
    {
        if (changingColor)
        {
            yield break;
        }
        changingColor = true;
        float counter = 0;

        while (counter < duration)
        {
            counter += Time.deltaTime;

            float colorTime = counter / duration;
            // Debug.Log(colorTime);

            //Change color
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(fromColor, toColor, counter / duration);
            //Wait for a frame
            yield return null;
        }
        changingColor = false;
    }
}
