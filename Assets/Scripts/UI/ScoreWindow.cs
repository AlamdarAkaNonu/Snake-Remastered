using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreWindow : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI scoreText;
   
    public static int score;

    private void Awake() {
        OnAwakeSetScoreZero();
        scoreText.GetComponent<TextMeshProUGUI>();
    }

    private void Update() {
        scoreText.text = score.ToString();
    }

    public static int AddScore() {
        score += 100;
        return score;
    }
    private void OnAwakeSetScoreZero() {
        score = 0;
    }
}


