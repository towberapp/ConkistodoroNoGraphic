using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersAudioController : MonoBehaviour
{
    private AudioSource leftAudioSource;
    private AudioSource rightudioSource;

    [Header("Setting takt")]
    [Range(0f, 1f)]
    [SerializeField] private float takt = 0.5f;

    [Header("Setting volume")]
    [Range(0f, 1f)]
    [SerializeField] private float firstChannel = 0.5f;
    [Range(0f, 1f)]
    [SerializeField] private float secondChannel = 0.2f;


    [Header("Audio")]
    [SerializeField] private AudioClip leftAudioClips;
    [SerializeField] private AudioClip rightAudioClips;

    [Header("Listner Object")]
    [SerializeField] private CharacterMovement move;    

    

    private IEnumerator coroutine;
    bool start = false;

    private void Awake()
    {        
        coroutine = WaitAndPrint();
        leftAudioSource = gameObject.AddComponent<AudioSource>();
        rightudioSource = gameObject.AddComponent<AudioSource>();

        leftAudioSource.playOnAwake = false;
        rightudioSource.playOnAwake = false;
    }

    private void Start()
    {
        leftAudioSource.clip = leftAudioClips;
        rightudioSource.clip = rightAudioClips; 
    }

    private void Update()
    {
        if (move.Velocity == Vector2.zero && start) 
        {
            start = false;
            StopCoroutine(coroutine);            
        }        

        if (move.Velocity != Vector2.zero && !start)
        {
            start = true;
            StartCoroutine(coroutine);
        }        
    }

    public IEnumerator WaitAndPrint()
    {
        while (true)
        {
            leftAudioSource.volume = firstChannel;
            leftAudioSource.Play();
            yield return new WaitForSeconds(takt);
            rightudioSource.volume = secondChannel;
            rightudioSource.Play();
            yield return new WaitForSeconds(takt);
        }
    }
}
