using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public String name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float Volume;

    [Range(0.1f, 3f)]
    public float Pitch;
    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
