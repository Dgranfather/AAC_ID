using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource theAudio;
    public AudioClip[] soundQueue;
    private int soundIndex = 0;

    private bool onPlay = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayAudio(AudioClip clip)
    {
        theAudio.PlayOneShot(clip);
    }

    public void StopAudio()
    {
        theAudio.Stop();
    }

    public void PlayQueue()
    {
        if(!onPlay) StartCoroutine(PlayQueueCoroutine());
    }

    public void AddSoundToQueue(AudioClip clip)
    {
        Array.Resize(ref soundQueue, soundQueue.Length + 1);
        soundQueue[soundQueue.Length - 1] = clip;
    }

    public void ClearQueue()
    {
        soundQueue = new AudioClip[0];
        soundIndex = 0;
    }

    private IEnumerator PlayQueueCoroutine()
    {
        onPlay = true;
        for (int i = soundIndex; i < soundQueue.Length; i++)
        {
            theAudio.clip = soundQueue[i];
            theAudio.Play();
            soundIndex = i + 1;

            // Wait until the current clip has finished playing before playing the next clip
            yield return new WaitForSeconds(theAudio.clip.length);
        }

        // Reset the soundIndex so the queue can be played again
        soundIndex = 0;
        onPlay = false;
    }
}
