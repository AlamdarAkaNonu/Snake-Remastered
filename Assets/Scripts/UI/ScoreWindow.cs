using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreWindow : MonoBehaviour {
    [SerializeField] private  TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
        
    public static int score;
    public static int highScore;

    
    private void Awake() {
        OnAwakeSetScoreZero();
        score += 100;
        scoreText.text = score.ToString();
        highScoreText.text = highScore.ToString();
    }

    private void Update() {
       
    }

    public  int AddScore() {

        if (score > highScore) {
            highScore = score;
        }

        return score;
    }
    private void OnAwakeSetScoreZero() {
        score = 0;
    }
    

}           