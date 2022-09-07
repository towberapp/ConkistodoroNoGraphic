using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezdeystvieMusic : MonoBehaviour
{
    [Header("Sound")]
    [SerializeField] private List<AudioClip> clips;

    [Header("Player")]
    [SerializeField] private AudioSource audioSource;

    [Header("Range in seconds")]
    [SerializeField] private float minPause;
    [SerializeField] private float maxPause;

    [Header("Curves of sound")]
    [SerializeField]
    private AnimationCurve _Appear = null, _Disappear = null;

    private IEnumerator coroutine;
    private AudioSource[] audioSources;
    private Coroutine volume = null;


    private void Awake()
    {
        audioSources = FindObjectsOfType<AudioSource>(); 
    }

    private bool CheckAllAudio()
    {
        foreach (var item in audioSources)
        {
            if (item.isPlaying)
            {
                return true;
            }
        }
        return false;
    }


    private void Start()
    {
        coroutine = PlayRandomMusic();        
        StartCoroutine(coroutine);
    }

    public void ResetCounter()
    {
        if (clips.Count == 0) return;
         
        if (audioSource.isPlaying)
        {
            //DisableVolume();            
        }

        //Debug.Log("STOP / START COURUTINES");
        StopCoroutine(coroutine);

        coroutine = PlayRandomMusic();
        StartCoroutine(coroutine);
    }


    IEnumerator PlayRandomMusic ()
    {
        float rnd = Random.Range(minPause, maxPause);
        yield return new WaitForSeconds(rnd);


        //bool checkAudio = CheckAllAudio();
        //bool checkAudio = CheckAllAudio();

        while (audioSource.isPlaying) {
            yield return new WaitForSeconds(3.0f);
            //checkAudio = CheckAllAudio();
            Debug.Log("CHECK AUDIO");
        }
      
        int clipRnd = Random.Range(0, clips.Count);

        Debug.Log("PLAY AUDIO");

        audioSource.clip = clips[clipRnd];
        EnableVolume();
        audioSource.Play();

        yield return new WaitForSeconds(audioSource.clip.length);
        ResetCounter();
    }


    IEnumerator SetVolume(AnimationCurve curve, bool onSound)
    {
        float time = Time.time;
        while (Time.time - time <= curve.keys[1].time)
        {
            float newValue = curve.Evaluate(Time.time - time);
            audioSource.volume = newValue < 0 ? 0 : newValue; //на случай, если в кривой косяк
            yield return null;
        }
        volume = null;

        if (!onSound)
        {
            audioSource.Stop();
        }
    }


    private void EnableVolume()
    {
        if (volume != null)
        {
            StopCoroutine(volume);
            volume = null;
        }
        volume = StartCoroutine(SetVolume(_Appear, true));
    }

    private void DisableVolume()
    {
        if (volume != null)
        {
            StopCoroutine(volume);
            volume = null;
        }
        volume = StartCoroutine(SetVolume(_Disappear, false));
    }
}
