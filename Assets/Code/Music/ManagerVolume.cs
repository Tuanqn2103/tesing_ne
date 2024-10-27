using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoundManager : MonoBehaviour
{
    public Sprite soundOnImage;
    public Sprite soundOfImage;
    public Button button;
    private bool isOn = true;
    public AudioSource audioSourceMusic;
    public AudioSource audioSourceSFX;
    void Start()
    {

    }
    void Update()
    {

    }

    public void ButtonClisked()
    {
        if (isOn)
        {
            button.image.sprite = soundOfImage;
            isOn = false;
            audioSourceMusic.mute = true;
            audioSourceSFX.mute = true;

        }
        else
        {
            button.image.sprite = soundOnImage;
            isOn = true;
            audioSourceMusic.mute = false;
            audioSourceSFX.mute = false;

        }
    }


}