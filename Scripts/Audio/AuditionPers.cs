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

    private void Awake()
    {
        leftAudioSource = gameObject.AddComponent<AudioSource>();
        rightudioSource = gameObject.AddComponent<AudioSource>();
        SetStandart();
    }

    public void SetClip(AudioClip clip)
    {
        SetClipInAudioSorce(clip, leftAudioSource);
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
            leftAudioSource.Play();
        } else
        {
            rightudioSource.Play();
        }
        check = !check;
    }

    private void SetClipInAudioSorce(AudioClip clip, AudioSource source)
    {
        if (clip != null)
            source.clip = clip;
    }

    // test
    // new main


}
