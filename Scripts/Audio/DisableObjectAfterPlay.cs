using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableObjectAfterPlay : MonoBehaviour
{
    [SerializeField] private readonly string state = "off";
    
    AudioSource audioSource;
    ObjectProgressManager objectProgress;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        objectProgress = GetComponent<ObjectProgressManager>();
    }

    public void StartPlaying()
    {
        StartCoroutine(CheckStopPlay());
    }

    IEnumerator CheckStopPlay()
    {
        while (audioSource.isPlaying)
        {
            yield return new WaitForSeconds(1f);
        }

        SetState();
    }

    private void SetState()
    {
        Debug.Log("SET STATE");
        objectProgress.SetState(state);
    }
}
