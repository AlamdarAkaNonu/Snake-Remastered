using TMPro;
using UnityEngine;

public class ScoreWindow : MonoBehaviour {

    private const string HighScoreKey = "HighScoreValueText";

    [SerializeField] private  TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
        
    public static int score;
    public static int highScore;


    private void Start() {
        OnAwakeSetScoreZero();

        highScore = PlayerPrefs.GetInt(HighScoreKey, highScore);
        PlayerPrefs.Save();
    }

    private void Update() {
        scoreText.text = score.ToString();
        highScoreText.text = highScore.ToString();

        if (score > highScore) {
            highScore = score;
            PlayerPrefs.SetInt(HighScoreKey, score);
        }
    }

    public  int AddScore() {
        score += 100;
        return score;
    }
    private void OnAwakeSetScoreZero() {
        score = 0;
    }
    

}           