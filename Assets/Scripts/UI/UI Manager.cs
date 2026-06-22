using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public static UIManager Instance { get; private set; }

    [SerializeField] private PausedWindow pausedWindow;

    [SerializeField] private GameOverWindow gameOverWindow;


    //GameOverWindowButton events
    public event EventHandler OnClickTryAgainButton;
    public event EventHandler OnClickMainMenuButton;


    //PausedWindow Button events
    public event EventHandler OnClickResumeButton;
    public event EventHandler OnClickExitButton;



    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }
    private void Start() {
        pausedWindow.HidePausedWindow();


        //Game Over MainMenu Button
        OnClickMainMenuButton += gameOverWindow.OnClickMainMenuButton;
        gameOverWindow.mainMenuButton.onClick.AddListener(() => {
            OnClickMainMenuButton?.Invoke(this, EventArgs.Empty);
        });
        //Game Over TryAgain Button
        OnClickTryAgainButton += gameOverWindow.OnClickTryAgainButton;
        gameOverWindow.tryAgainButton.onClick.AddListener(() => {
            OnClickTryAgainButton?.Invoke(this, EventArgs.Empty);
        });

        OnClickResumeButton += HandleResumeButton;
        pausedWindow.resumeButton.onClick.AddListener(() => {
            OnClickResumeButton?.Invoke(this, EventArgs.Empty);
        });

        OnClickExitButton += HandlePausedWindowExitButton;
        pausedWindow.exitButton.onClick.AddListener(() => {
            OnClickExitButton?.Invoke(this, EventArgs.Empty);
        });
    }

    private void HandlePausedWindowExitButton(object sender, EventArgs e) {
        Application.Quit();
        Debug.Log("Exit Button Clicked");
    }

    private void HandleResumeButton(object sender, EventArgs e) {
        GameManager.Instance.isPausedWindowActive = false;
        pausedWindow.HidePausedWindow();
        Time.timeScale = 1f;
    }

    private void Update() {
        if (GameManager.Instance.isPausedWindowActive == true) {
            pausedWindow.gameObject.SetActive(true);

        }
        else if (GameManager.Instance.isPausedWindowActive == false) {
            pausedWindow.gameObject.SetActive(false);
        }


        if (GameManager.Instance.state == GameManager.State.Alive) {
            gameOverWindow.HideGameOverWindow();
        }
        else if (GameManager.Instance.state == GameManager.State.Dead) {

            gameOverWindow.ShowGameOverWindow();
            gameOverWindow.UpdateGameOverScore();
        }
    }
}
