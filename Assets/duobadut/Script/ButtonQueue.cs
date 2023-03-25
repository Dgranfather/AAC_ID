using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonQueue : MonoBehaviour
{
    public GameObject copyButton;
    public AudioClip buttonAudio;

    public AudioManager theAudioManager;
    private AudioSource theAudio;

    public static bool isPlayAudioOnClick = true;

    private void Start()
    {
        theAudio = GetComponent<AudioSource>();
    }

    public void OnButtonClick()
    {
        copyButton = gameObject;
        GameObject newButton = Instantiate(copyButton, ShowScreenManager.ShowScreen);
        newButton.GetComponent<Button>().interactable = false;

        if (isPlayAudioOnClick)
        {
            PlayAudio();
        }

        theAudioManager.AddSoundToQueue(buttonAudio);
    }

    public void PlayAudio()
    {
        theAudio.PlayOneShot(buttonAudio);
    }

    public void StopAudio()
    {
        theAudio.Stop();
    }
}
