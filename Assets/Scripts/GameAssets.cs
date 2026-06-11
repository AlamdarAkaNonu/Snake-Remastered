using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameAssets : MonoBehaviour {
    public static GameAssets Instance { get; private set; }

    public Sprite snakeHeadSprite;
    public Sprite foodSprite;
    public Sprite snakeBodyPartSprite;

    
    private void Awake() {
        Instance = this;
    }
}
