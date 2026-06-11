using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class MainMenuWindow : MonoBehaviour {

    public event EventHandler onClickPlayButton;
    public event EventHandler onClickQuitButton;


    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    private void Awake() {


    }
    private void Start() {
        onClickPlayButton += Loader.LoadScene;
        playButton.onClick.AddListener(() => {
            onClickPlayButton?.Invoke(this, EventArgs.Empty);
            Debug.Log("Play Button Clicked");
        });

        onClickQuitButton += QuitButton;
        quitButton.onClick.AddListener(() => {
            onClickQuitButton?.Invoke(this, EventArgs.Empty);
        });
    }

    private void QuitButton(object sender, EventArgs e) {
        Application.Quit();
        Debug.Log("Game Quit");
    }

}
