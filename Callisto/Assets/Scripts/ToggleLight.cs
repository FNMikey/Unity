using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleLight : MonoBehaviour
{
    public Light light;

    void Start()
    {
        light = GetComponent<Light>();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.L))
        {
            light.enabled = !light.enabled;
        }
    }
}
