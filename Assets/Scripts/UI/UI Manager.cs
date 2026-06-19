using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    public static UIManager Instance { get; private set; }

    public GameObject gameOverWindow;
    
    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }
    private void Update() {
       // GameOverWindow.Instance.UpdateGameOverHighScore();

        if (GameManager.Instance.state == GameManager.State.Alive) {
            GameOverWindow.Instance.HideGameOverWindow();
        }
        else if(GameManager.Instance.state == GameManager.State.Dead) {

            GameOverWindow.Instance.ShowGameOverWindow();
            GameOverWindow.Instance.UpdateGameOverScore();
            
            
        }
    }
}
