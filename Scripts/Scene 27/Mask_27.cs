using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Mask_27 : AnimatorObject
{
    [SerializeField] private Sprite _MaskSprite = null;
    [SerializeField] private float _SwitchDuration = 0.25f;
    private Sprite _PrevSprite;
    private SpriteMask _Mask;
    private Coroutine _Changing;
    private float _Time
    {
        get => animator.GetFloat("Time");
        set => animator.SetFloat("Time", value);
    }

    void Start()
    {
        _Mask = GetComponent<SpriteMask>();
    }

    private IEnumerator ChangingTime(float target, float duration)
    {
        float startTime = Time.time;
        float startValue = _Time;
        while (Time.time - startTime < duration)
        {
            _Time = startValue + (target - startValue) * (Time.time - startTime) / duration;
            yield return null;
        }
    }

    public void ChangeTo(float target)
    {
        if (_Changing != null)
            StopCoroutine(_Changing);
        _Changing = StartCoroutine(ChangingTime(target, _SwitchDuration));
    }
    void Update()
    {
        if (_MaskSprite != _PrevSprite)
        {
            _Mask.sprite = _MaskSprite;
            _PrevSprite = _MaskSprite;
        }
    }
}
