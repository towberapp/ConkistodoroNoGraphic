using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : MonoBehaviour 
{
    [SerializeField] private AudioSource _AudioSource = null;

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
