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

    public bool escButtonWasPressed = false;

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
                HandleEscapeButton();
                break;
            case State.Dead:                
                break;

        }
    }
                          
    public void SetGameStateToDead() {
        state = State.Dead;
    }
    private void HandleEscapeButton() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            SoundManager.PlaySound(SoundManager.Sound.onButtonClick);   
            escButtonWasPressed = true;
            if (escButtonWasPressed == true) {
                Time.timeScale = 0f;
            }
        }
            
    }
}
