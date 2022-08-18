using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MainThemeAudio : MonoBehaviour
{
    [SerializeField]
    private ClipParams _StartClip = null;

    [SerializeField]
    private AnimationCurve _Appear = null, _Disappear = null;

    [SerializeField]
    private float _MinWait = 0, _MaxWait =1;

    [SerializeField]
    private List<ClipParams> _Clips = null;

    private int _CurrenClip = 0;

    
    private int CurrentClip
    {
        get
        {
            return _CurrenClip;
        }
        set // сделал только для более удобного перебора
        {
            if (value >= _Clips.Count)
                _CurrenClip = 0;
            else if (value < 0)
                _CurrenClip = _Clips.Count - 1;
            else
                _CurrenClip = value;
        }
    }

    private AudioSource audioSource = null;

    private Coroutine volume = null, clipSwitching = null;

    IEnumerator SetVolume(AnimationCurve curve)
    {
        float time = Time.time;
        while (Time.time - time <= curve.keys[1].time)
        {
            float newValue = curve.Evaluate(Time.time - time);
            audioSource.volume = newValue < 0 ? 0 : newValue; //на случай, если в кривой косяк
            yield return null;
        }
        volume = null;
    }

    public void EnableVolume()
    {
        if (volume != null)
        {
            StopCoroutine(volume);
            volume = null;
        }
        volume = StartCoroutine(SetVolume(_Appear));
    }

    public void DisableVolume()
    {
        if (volume != null)
        {
            StopCoroutine(volume);
            volume = null;
        }
        volume = StartCoroutine(SetVolume(_Disappear));
    }

    //Реализовал смену клипа через корутин, чтобы можно было дождаться,
    //пока звук убавиться и только потом переключть
    private IEnumerator SwitchClip(ClipParams clipParam)
    {
        if (audioSource.volume != 0 && volume == null)
            DisableVolume();
        while (volume != null)
        {
            yield return null;
        }
        audioSource.clip = clipParam.Clip;
        audioSource.Play();
        //обновляем значения ключей в кривых, поскольку максимальная громкость у каждого клипа своя
        float keyTime = _Appear.keys[1].time;
        _Appear.RemoveKey(1); 
        _Disappear.RemoveKey(0); // пока я не удалил старые ключи, новые не обновлялись
        _Disappear.AddKey(new Keyframe(0, clipParam.MaxVolume));
        _Appear.AddKey(new Keyframe(keyTime, clipParam.MaxVolume));
        EnableVolume();
        clipSwitching = null;
    }

    public void SetClip(ClipParams clipParam)
    {
        if (clipSwitching != null)
        {
            StopCoroutine(clipSwitching);
            clipSwitching = null;
        }
        clipSwitching = StartCoroutine(SwitchClip(clipParam));
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (_StartClip != null)
        {
            audioSource.clip = _StartClip.Clip;
            SetClip(_StartClip);
        }
        StartCoroutine(Monitoring());
    }

    private IEnumerator Monitoring()
    {
        float timeToEnd = 0;
        while (true)
        {
            if (audioSource.clip == null)
            {
                audioSource.volume = 0;
                SetClip(_Clips[CurrentClip++]);
                yield return null;
                continue;
            }
            timeToEnd = audioSource.clip.length - audioSource.time;
            //изначально делал проверку на равенство с 0 , но из-за случаев,
            //когда audioSource.time сделал проверку такой
            if (timeToEnd <= 0.1f)
            {
                audioSource.clip = null;
                audioSource.Stop();
                yield return new WaitForSeconds(Random.Range(_MinWait, _MaxWait));
            }
            //disappear.keys[1].time , чтобы можно было спокойно изменять кривую в редакторе и не трогать код
            //т.е. мы начинаем выключение в тот момент,когда времени от клипа осталось как раз на период выключения
            else if (timeToEnd <= _Disappear.keys[1].time && volume == null)
            {
                DisableVolume();
            }
            yield return null;
        }
    }

    
}
