using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour
{
    public AudioSource musicAudioSource;
    public AudioSource SFXAudioSource;

    public AudioClip musicClip;
    public AudioClip coinClip;

    void Start(){
        musicAudioSource.clip = musicClip;
        musicAudioSource.Play();
    }
    public void PlayThisSoundEffect(){
        SFXAudioSource.Play();
    }
}
