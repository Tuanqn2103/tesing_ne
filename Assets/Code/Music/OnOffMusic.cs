using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffMusic : MonoBehaviour
{
    [SerializeField] AudioSource music;
    [SerializeField] AudioSource SFX;

    public void OnMusic(){
        music.Play();
        SFX.Play();
    }
    public void OffMusic(){
        music.Stop();
        SFX.Stop();
    }
}
