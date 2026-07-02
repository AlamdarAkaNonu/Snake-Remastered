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
            SoundManager.PlaySound(SoundManager.Sound.onButtonClick);
            DontDestroyOnLoad(SoundManager.soundGameObject);
        });
        //Game Over TryAgain Button
        OnClickTryAgainButton += gameOverWindow.OnClickTryAgainButton;
        gameOverWindow.tryAgainButton.onClick.AddListener(() => {
            OnClickTryAgainButton?.Invoke(this, EventArgs.Empty);
            SoundManager.PlaySound(SoundManager.Sound.onButtonClick);
            DontDestroyOnLoad(SoundManager.soundGameObject);
        });
        //Paused Window Resume button
        OnClickResumeButton += HandleResumeButton;
        pausedWindow.resumeButton.onClick.AddListener(() => {
            OnClickResumeButton?.Invoke(this, EventArgs.Empty);
            SoundManager.PlaySound(SoundManager.Sound.onButtonClickBack);
        });
        //Paused Window Exit button
        OnClickExitButton += HandlePausedWindowExitButton;
        pausedWindow.exitButton.onClick.AddListener(() => {
            OnClickExitButton?.Invoke(this, EventArgs.Empty);
            SoundManager.PlaySound(SoundManager.Sound.onButtonClickBack);
           DontDestroyOnLoad(SoundManager.soundGameObject);
        });
    }

    private void HandlePausedWindowExitButton(object sender, EventArgs e) {
        Application.Quit();
        DontDestroyOnLoad(SoundManager.soundGameObject);
        Debug.Log("Exit Button Clicked");
        
    }

    private void HandleResumeButton(object sender, EventArgs e) {
        GameManager.Instance.escButtonWasPressed = false;
        pausedWindow.HidePausedWindow();
        Time.timeScale = 1f;
    }

    private void Update() {
        if (GameManager.Instance.escButtonWasPressed == true) {
            pausedWindow.gameObject.SetActive(true);
            
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
