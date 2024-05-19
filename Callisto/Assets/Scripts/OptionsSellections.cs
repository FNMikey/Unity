using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsSellections : MonoBehaviour
{
    public GameObject music;
    public GameObject graphics;

    public void ChangeTabOnMusic()
    {
        graphics.SetActive(false);
        music.SetActive(true);
    }

    public void ChangeTabOnGraphics()
    {
        music.SetActive(false);
        graphics.SetActive(true);
    }
    void Start()
    {
        ChangeTabOnGraphics();
    }
}
