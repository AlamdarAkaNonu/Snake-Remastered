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

    public GameObject gameOverWindow;


    //GameOverWindowButton events
    public event EventHandler OnClickTryAgainButton;
    public event EventHandler OnClickMainMenuButton;



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
        OnClickMainMenuButton += GameOverWindow.Instance.OnClickMainMenuButton;
        GameOverWindow.Instance.mainMenuButton.onClick.AddListener(() => {
            OnClickMainMenuButton?.Invoke(this, EventArgs.Empty);
        });
        //Game Over TryAgain Button
        OnClickTryAgainButton += GameOverWindow.Instance.OnClickTryAgainButton;
        GameOverWindow.Instance.tryAgainButton.onClick.AddListener(() => {
            OnClickTryAgainButton?.Invoke(this, EventArgs.Empty);
        });

    }
    private void Update() {
        if (GameManager.Instance.isPausedWindowActive == true) {
            pausedWindow.gameObject.SetActive(true);

        }
        else if (GameManager.Instance.isPausedWindowActive == false) {
            pausedWindow.gameObject.SetActive(false);
        }


        if (GameManager.Instance.state == GameManager.State.Alive) {
            GameOverWindow.Instance.HideGameOverWindow();
        }
        else if(GameManager.Instance.state == GameManager.State.Dead) {

            GameOverWindow.Instance.ShowGameOverWindow();
            GameOverWindow.Instance.UpdateGameOverScore();
            
            
        }
    }
}
