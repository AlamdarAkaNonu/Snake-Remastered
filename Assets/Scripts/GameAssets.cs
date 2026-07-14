using UnityEngine;

public class GameAssets : MonoBehaviour {
    public static GameAssets Instance { get; private set; }

    public Sprite snakeHeadSprite;
    public Sprite foodSprite;
    public Sprite snakeBodyPartSprite;
    
    private void Awake() {
        Instance = this;
    }

    
    public SoundAudioClip[] soundAudioClipArray;


    [System.Serializable]
    public class SoundAudioClip {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }
}
