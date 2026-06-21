using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverWindow : MonoBehaviour {
    [Header("texts")]
    [SerializeField] private TextMeshProUGUI gameOverScoreText;

    [Header("Buttons")]
    public Button tryAgainButton;
    public Button mainMenuButton;

    private void Awake() {
    }

    public void ShowGameOverWindow() {
        gameObject.SetActive(true);
    }

    public void HideGameOverWindow() {
        gameObject.SetActive(false);
    }
    //Game Over Try Again Button 
    public void OnClickTryAgainButton(object sender, EventArgs e) {
        if (GameManager.Instance.state == GameManager.State.Dead) {
            SceneManager.LoadScene(Loader.Scene.GameScene.ToString());
        }
    }
    //Game Over MainMenu Button
    public void OnClickMainMenuButton(object sender, EventArgs e) {
        SceneManager.LoadScene(Loader.Scene.MainMenuScene.ToString());
    }
    //GameOverWindow score text update method
    public void UpdateGameOverScore() {
        gameOverScoreText.text = ScoreWindow.score.ToString();
    }

}

