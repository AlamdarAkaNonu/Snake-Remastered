using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausedWindow : MonoBehaviour {

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    
    private void Update() {

    }

    public void HidePausedWindow() {
        gameObject.SetActive(false);
    }
}
