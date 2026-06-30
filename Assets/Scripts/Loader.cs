using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loader : MonoBehaviour {
    public static Loader Instance { get; private set; }

    private static Action loaderCallbackAction;
    
    private static float coroutineTimer = 1f;

    private static float timer = 0f;
    private static float timerMax = 1f;



    public enum Scene {
        GameScene,
        LoadingScene,
        MainMenuScene,
    }
    private void Awake() {
        Instance = this;
    }
    public static void ManageScene(object sender, EventArgs e) {
        loaderCallbackAction = () => {
            coroutineTimer += Time.deltaTime;
            if (coroutineTimer > timerMax) {
                coroutineTimer = 0f;
               // MainMenuWindow.
            }
            SceneManager.LoadScene(Scene.GameScene.ToString());
        };
        SceneManager.LoadScene(Scene.LoadingScene.ToString());

    }
    public static void LoaderCallBack() {
        if (loaderCallbackAction != null) {//Gamescene save hwa.
            loaderCallbackAction();
            loaderCallbackAction = null;
        }

    }
    public static IEnumerator WaitForSecondsThenLoadGameScene() {
        yield return new WaitForSeconds(coroutineTimer);
        Loader.LoaderCallBack();
    }
}
