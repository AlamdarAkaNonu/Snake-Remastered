using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;

public class GameOverWindow : MonoBehaviour {

    public static GameOverWindow Instance;
    //[SerializeField] private GameObject gameOverWidow;

    public Button tryAgainButton;
    public Button mainMenuButton;

    private void Awake() {
        Instance = this;
    }
    private void Update() {

    }

    public void ShowGameOverWindow() {
           UIManager.Instance.gameOverWindow.SetActive(true);
    }

    public void HideGameOverWindow() {
        UIManager.Instance.gameOverWindow.SetActive(false);
    }

    public void OnClickTryAgainButton(object sender, EventArgs e) {
         if(GameManager.Instance.state == GameManager.State.Dead) {
            SceneManager.LoadScene(Loader.Scene.GameScene.ToString());
        }
    }

    public void OnClickMainMenuButton(object sender, EventArgs e) {
         if(GameManager.Instance.state == GameManager.State.Dead) {
           // SceneManager.LoadScene(Loader.Scene.GameScene.ToString());
           //Create Main menu scene and load it here
            // Debug.Log("Main Menu Button Clicked");
        }
    }
}
