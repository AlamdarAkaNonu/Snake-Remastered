using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreWindow : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI scoreText;
    private static int score;

    private void Awake() {
        OnServerInitialized();
        scoreText.GetComponent<TextMeshProUGUI>();
    }

    private void Update() {
        scoreText.text = score.ToString();
    }

    public static void AddScore() {
        score += 100;
    }
    private void OnServerInitialized() {
        score = 0;
    }
}


