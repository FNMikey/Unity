using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject uiPanel; 

void Start() {
    if (uiPanel != null) {
        uiPanel.SetActive(false);
    }
}

void Update() {
    if (Input.GetKeyDown(KeyCode.N)) {
        ToggleUIPanel();
    }
}

void ToggleUIPanel() {
    if (uiPanel != null) {
        uiPanel.SetActive(!uiPanel.activeSelf);
    }
}

}
