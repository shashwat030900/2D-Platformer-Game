using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public Sound[] sounds;

    private void Awake() {
        foreach (Sound s in sounds){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.Volume;
            s.source.pitch = s.Pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name){

        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
    // private static SoundManager instance;
    // public static SoundManager Instance {get {return instance}}

    // public SoundType[] sounds; 
    // void Awake()
    // {
    //     if(instance == null){
    //         instance = this;
    //         DontDestroyOnLoad(gameObject);
    //     }
    //     else{
    //         Destroy(gameObject);
    //     }
    // }

    // public void Play(Sounds sound)
    // {
    //     AudioClip clip = getSoundClip(sound);
    // }

    // private AudioClip getSoundClip(Sounds sound)
    // {
    //     SoundType item = Array.Find(Sounds, item => item.soundType == sound);    
    //     if(item != null)
    //     {
    //         soundEffect
    //     }
    // }
}
[Serializable]
public class SoundType
{
    public Sounds soundType;
    public AudioClip soundClip;
}
public enum Sounds{
    ButtonClick,
    PlayerMove,
    PlayerDeath,
    EnemyDeath
}