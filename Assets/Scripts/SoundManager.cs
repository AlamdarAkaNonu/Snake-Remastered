using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour{

    public static GameObject soundGameObject;
    public enum Sound{
        onButtonClick,
        onButtonClickBack,
        onSnakeDied,
        onSnakeEat,
                
    }
    
    public static void PlaySound(Sound sound) {

        soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(GetAudioClip(sound));
    }
    public static AudioClip GetAudioClip(Sound sound) {
        foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.Instance.soundAudioClipArray) {
            if(soundAudioClip.sound == sound) {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound" +  sound + "didn't found");
        return null;
    }
}