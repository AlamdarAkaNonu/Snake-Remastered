using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenuWindow : MonoBehaviour {

    public event EventHandler onClickPlayButton;
    public event EventHandler onClickQuitButton;
    public event EventHandler onClickHowToPlayButton;
    public event EventHandler onClickhowToPlayBackButton;
   
    [Header("Main Menu Window Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button howtoPlayButton;

    [Header("HowToPlayWindow")]
    [SerializeField] private GameObject howToPlayWindow;
    [SerializeField] private Button howToPlayBackButton;

    private void Start() {

        HideHowToPlayWindow();

        //Play Button 
        onClickPlayButton += Loader.ManageScene;
        playButton.onClick.AddListener(() => {
            onClickPlayButton?.Invoke(this, EventArgs.Empty);            
        });

        //Quit Button
        onClickQuitButton += HandleQuitButton;
        quitButton.onClick.AddListener(() => {
            onClickQuitButton?.Invoke(this, EventArgs.Empty);
        });

        //HowToPlay Button
        onClickHowToPlayButton += HandleHowToPlayButton;
        howtoPlayButton.onClick.AddListener(() => {
            onClickHowToPlayButton?.Invoke(this, EventArgs.Empty);
        });
        //HowToPlayBackButton
        onClickhowToPlayBackButton += HandleHowToPlayBackButton;
        howToPlayBackButton.onClick.AddListener(() => {
            onClickhowToPlayBackButton?.Invoke(this, EventArgs.Empty);
            howToPlayWindow.SetActive(false);
        });
    }

    private void HandleHowToPlayButton(object sender, EventArgs e) {
        ShowHowToPlayWindow();
    }

    private void HandleHowToPlayBackButton(object sender, EventArgs e) {
        HideHowToPlayWindow();
    }

    private void HandleQuitButton(object sender, EventArgs e) {
        Application.Quit();
    }
    private void ShowHowToPlayWindow() {
        howToPlayWindow.SetActive(true);
    }
    private void HideHowToPlayWindow() {
        howToPlayWindow.SetActive(false);
    }
}

