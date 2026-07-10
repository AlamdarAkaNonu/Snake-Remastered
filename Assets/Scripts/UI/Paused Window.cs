using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausedWindow : MonoBehaviour {

    public Button resumeButton;
    public Button exitButton;

    public bool escButtonWasPressed = false;

    public void HidePausedWindow() {
        gameObject.SetActive(false);
    }

    public void HandleEscapeButton() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            escButtonWasPressed = true;
            SoundManager.PlaySound(SoundManager.Sound.onButtonClick);
            if (escButtonWasPressed == true) {
                Time.timeScale = 0f;
                escButtonWasPressed = false;

            }
        }
    }
}


