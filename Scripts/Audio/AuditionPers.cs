using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuditionPers : MonoBehaviour
{
    [System.NonSerialized]
    public AudioSource[] mainAudioSource = new AudioSource[3];

    [SerializeField] private AudioClip audioClips;


    private void Awake()
    {
        for (int i = 0; i < mainAudioSource.Length; i++)
        {
            mainAudioSource[i] = gameObject.AddComponent<AudioSource>();
            mainAudioSource[i].playOnAwake = false;
        }
        SetStandart();
    }

    public void SetClip(AudioClip clip)
    {
        SetClipInAudioSorce(clip);
    }

    public void SetStandart()
    {
        SetClipInAudioSorce(audioClips);
    }

    public void PlayPesrAudio()
    {
        bool isPlay = false;
        int count = 0;
        foreach (var item in mainAudioSource)
        {
            if (!item.isPlaying && !isPlay)
            {
                item.Play();
                isPlay = true;
            }
            count++;
        }
    }

    private void SetClipInAudioSorce(AudioClip clip)
    {
        foreach (var item in mainAudioSource)
        {
            item.clip = clip;
        }
    }


}
