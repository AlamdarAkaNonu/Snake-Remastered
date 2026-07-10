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

    [SerializeField] private PausedWindow pausedWindow;

    public State state;
    public enum State {
        Alive,
        Dead,
        Paused,
    }
    private void Awake() {
        Instance = this;

    }
    private void Start() {
        state = State.Alive;

    }
    private void Update() {
        switch (state) {

            case State.Alive:
                Snake.snake.HandleInput();
                Snake.snake.HandleGridMovenment();
                pausedWindow.HandleEscapeButton();
                break;
            case State.Dead:
                break;

        }
    }
                          
    public void SetGameStateToDead() {
        state = State.Dead;
    }
    
}
