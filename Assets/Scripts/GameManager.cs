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

    //GameOverWindowButton events
    public event EventHandler OnClickTryAgainButton;
    public event EventHandler OnClickMainMenuButton;

    //[SerializeField] private Button playButton;


    public State state;
    public enum State {
        Alive,
        Dead
    }
    private void Awake() {
        Instance = this;

    }
    private void Start() {
        state = State.Alive;

        //MainMenu Button
        OnClickMainMenuButton += GameOverWindow.Instance.OnClickMainMenuButton;
        GameOverWindow.Instance.mainMenuButton.onClick.AddListener(() => {
            OnClickMainMenuButton?.Invoke(this, EventArgs.Empty);
        });
        //TryAgain Button
        OnClickTryAgainButton += GameOverWindow.Instance.OnClickTryAgainButton;
        GameOverWindow.Instance.tryAgainButton.onClick.AddListener(() => {
            OnClickTryAgainButton?.Invoke(this, EventArgs.Empty);
        });
    }


    private void Update() {
        switch (state) {
            case State.Alive:
                Snake.snake.HandleInput();
                Snake.snake.HandleGridMovenment();
                break;
            case State.Dead:
                break;

        }
    }
    public void SetStateToGameOver() {
        state = State.Dead;
    }
}

