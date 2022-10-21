using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : MonoBehaviour 
{
    private AudioSource _AudioSource;

    private void Awake()
    {
        _AudioSource = gameObject.GetComponent<AudioSource>();

        if ( _AudioSource == null)
        {
            _AudioSource = gameObject.AddComponent<AudioSource>();
        }        
    }

    public void Play(AudioClip clip)
    {
        if (_AudioSource.clip != null && clip == null)
        {
            _AudioSource.Play();
            return;
        } 

        if (clip)
            _AudioSource.clip = clip;
        
        _AudioSource.Play();
    }

    /*
     GIT TEST
     */
}
