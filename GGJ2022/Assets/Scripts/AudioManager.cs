using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource effectsAudioSource = null;
    [SerializeField] AudioEffects audioEffects = null;
    static AudioManager instance = null;
    
    public static void Play(AudioEffect effect){
        instance?.audioEffects.Play(effect,instance.effectsAudioSource);
    }
    
    private void OnEnable(){
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        Destroy(this);
    }
    private void OnDisable(){
        if(instance == this){
            instance = null;
        }
    }
}
