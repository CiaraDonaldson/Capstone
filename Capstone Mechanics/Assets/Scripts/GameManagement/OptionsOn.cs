using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsOn : MonoBehaviour
{
    public GameObject GameMang;

    private void Start()
    {
        GameMang = GameObject.Find("GameManager");

    }
    public void ToggleOptions()
    {
        GameMang.GetComponent<PauseMenu>().optionsOpen = !GameMang.GetComponent<PauseMenu>().optionsOpen;
       
    }
}
