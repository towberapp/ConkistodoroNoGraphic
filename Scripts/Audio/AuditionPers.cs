using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuditionPers : MonoBehaviour
{
    AudioSource leftAudioSource;
    AudioSource rightudioSource;

    [SerializeField] private AudioClip leftAudioClips;
    [SerializeField] private AudioClip rightAudioClips;

    private bool check = true;

    private AudioSource[] sounds;

    private void Awake()
    {
        sounds = GetComponents<AudioSource>();

        if (sounds.Length == 0)
        {
            leftAudioSource = gameObject.AddComponent<AudioSource>();
            rightudioSource = gameObject.AddComponent<AudioSource>();
        } else
        {
            leftAudioSource = sounds[0];
            rightudioSource = sounds[1];
        }

        
        leftAudioSource.playOnAwake = false;
        rightudioSource.playOnAwake = false;

        SetStandart();
    }


    public void SetAudio_A(AudioClip clip)
    {
        SetClipInAudioSorce(clip, leftAudioSource);
    }

    public void SetAudio_B(AudioClip clip)
    {
        SetClipInAudioSorce(clip, rightudioSource);
    }

    public void SetStandart()
    {
        SetClipInAudioSorce(leftAudioClips, leftAudioSource);
        SetClipInAudioSorce(rightAudioClips, rightudioSource);
    }

    public void PlayPesrAudio()
    {

        if (check)
        {
            if (leftAudioSource.clip != null && !leftAudioSource.isPlaying)
                leftAudioSource.Play();
        }
        else
        {
            if (rightudioSource.clip != null && !rightudioSource.isPlaying)
                rightudioSource.Play();
        }
        check = !check;
    }

    private void SetClipInAudioSorce(AudioClip clip, AudioSource source)
    {
        if (clip != null)
            source.clip = clip;
    }


}
