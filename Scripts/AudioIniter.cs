using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioIniter : MonoBehaviour
{
    [SerializeField] private float _initFrames;
    void Start()
    {
        StartCoroutine(Init());
    }

    private IEnumerator Init()
    {
        var allAudio = FindObjectsOfType<AudioSource>();
        foreach (var audio in allAudio) audio.enabled = false;
        for (int i = 0; i < _initFrames; i++)
            yield return null;
        foreach (var audio in allAudio) audio.enabled = true;
    }
    
}
