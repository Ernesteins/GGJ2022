using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioEffect{attack,villager,win,trap}

[CreateAssetMenu(fileName = "new AudioEffects", menuName = "Audio/AudioEffects")]
public class AudioEffects : ScriptableObject
{
    [SerializeField] Sound attack = null;
    [SerializeField] Sound villager = null;
    [SerializeField] Sound win = null;
    [SerializeField] Sound trap = null;
    private Sound GetSound(AudioEffect effect){
        switch (effect)
        {
            case AudioEffect.attack: return attack;
            case AudioEffect.villager: return villager;
            case AudioEffect.win: return win;
            case AudioEffect.trap: return trap;
        }
        return null;
    }
   
    public void Play(AudioEffect effect, AudioSource source){
        Sound sound = GetSound(effect);
        source.clip = sound.clip;
        source.volume = sound.volume + UnityEngine.Random.Range(-sound.volumeVariation,sound.volumeVariation);
        source.pitch = sound.pitch + UnityEngine.Random.Range(-sound.pitchVariation,sound.pitchVariation);
        source.Play();
    }
    [System.Serializable]
    public class Sound{
        public AudioClip clip = null;
        [Range(0,1)] public float volume = 1f;
        [Range(0,1)] public float volumeVariation = 0.25f;
        [Range(0,1)] public float pitch = 1f;
        [Range(0,1)] public float pitchVariation = 0.25f;
    }
}
