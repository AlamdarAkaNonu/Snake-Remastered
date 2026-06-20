using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    [SerializeField] private PausedWindow pausedWindow;

    //GameOverWindowButton events
    public event EventHandler OnClickTryAgainButton;
    public event EventHandler OnClickMainMenuButton;

    public event EventHandler OnClickEscapeButton;

    //paused Window button events
    //public event EventHandler OnClickResumeButton;
    //public event EventHandler OnClickPausedWindowMainMenuButton;

    //[SerializeField] private Button playButton;


    public State state;
    public enum State {
        Alive,
        Dead,
        Paused,
    }
    private void Awake() {
        Instance = this;

    }
    private void Start() {
        state = State.Alive;

        //Game Over MainMenu Button
        OnClickMainMenuButton += GameOverWindow.Instance.OnClickMainMenuButton;
        GameOverWindow.Instance.mainMenuButton.onClick.AddListener(() => {
            OnClickMainMenuButton?.Invoke(this, EventArgs.Empty);
        });
        //Game Over TryAgain Button
        OnClickTryAgainButton += GameOverWindow.Instance.OnClickTryAgainButton;
        GameOverWindow.Instance.tryAgainButton.onClick.AddListener(() => {
            OnClickTryAgainButton?.Invoke(this, EventArgs.Empty);
        });

        OnClickEscapeButton += HandlePausedState;

        //Paused window main menu button
        //OnClickPausedWindowMainMenuButton += HandlePausedWindowMainMenuClick;

    }

    private void HandlePausedState(object sender, EventArgs e) {
        
    }

    //private void HandlePausedWindowMainMenuClick(object sender, EventArgs e) {
    //    pausedWindow.resumeButton.onClick.AddListener(() => {

    //    });
    //}

    private void Update() {
        switch (state) {
            case State.Alive:
                Snake.snake.HandleInput();
                Snake.snake.HandleGridMovenment();
                break;
            case State.Dead:
                break;

            case State.Paused:
                Time.timeScale = 0f;
                break;

        }
    }
    public void SetStateToGameOver() {
        state = State.Dead;
    }
}

