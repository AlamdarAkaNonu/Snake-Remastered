using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverWindow : MonoBehaviour {

    public static GameOverWindow Instance;
    [SerializeField] private TextMeshProUGUI GameOverScoreText;

    public Button tryAgainButton;
    public Button mainMenuButton;

    private void Awake() {
        Instance = this;
    }
    public void ShowGameOverWindow() {
        UIManager.Instance.gameOverWindow.SetActive(true);
    }

    public void HideGameOverWindow() {
        UIManager.Instance.gameOverWindow.SetActive(false);
    }

    public void OnClickTryAgainButton(object sender, EventArgs e) {
        if (GameManager.Instance.state == GameManager.State.Dead) {
            SceneManager.LoadScene(Loader.Scene.GameScene.ToString());
        }
    }

    public void OnClickMainMenuButton(object sender, EventArgs e) {
        SceneManager.LoadScene(Loader.Scene.MainMenuScene.ToString());
    }
    public void SetGameOverScoreText() {
        GameOverScoreText.text = ScoreWindow.score.ToString();

    }
}
