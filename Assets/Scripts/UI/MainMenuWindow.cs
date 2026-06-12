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
    public event EventHandler onClickTutorialButton;
    public event EventHandler onClickhowToPlayBackButton;

    [SerializeField] private GameObject howToPlayWindow;


    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;
    [Header("Main Menu Window")]
    [SerializeField] private Button tutorialButton;
    [SerializeField] private Button howToPlayBackButton;

    private void Awake() {

    }
    private void Start() {
        //if(GameManager.Instance.state == GameManager.State.Dead) {


        //    backButton.gameObject.SetActive(true);
        //} else {
        //    backButton.gameObject.SetActive(false);
        //}

        onClickPlayButton += Loader.LoadScene;
        playButton.onClick.AddListener(() => {
            onClickPlayButton?.Invoke(this, EventArgs.Empty);
            Debug.Log("Play Button Clicked");
        });

        onClickQuitButton += QuitButton;
        quitButton.onClick.AddListener(() => {
            onClickQuitButton?.Invoke(this, EventArgs.Empty);
        });
        onClickTutorialButton += TutorialButton;
        tutorialButton.onClick.AddListener(() => {
            onClickTutorialButton?.Invoke(this, EventArgs.Empty);
        }); 
    }

    private void TutorialButton(object sender, EventArgs e) {
        howToPlayWindow.SetActive(true);
    }

    private void QuitButton(object sender, EventArgs e) {
        Application.Quit();
    }

}
