using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV_36 : AnimatorObject
{
    [SerializeField] private float[] _frequences;
    [SerializeField] private GameEvent[] _events;
    private bool IsAppeared;

    public void Appear() => animator.SetBool(nameof(IsAppeared), IsAppeared = true);
    public void Disappear() => animator.SetBool(nameof(IsAppeared), IsAppeared = false);

    private IEnumerator Job(float frequence, GameEvent someJob)
    {
        if (frequence == 0)
            yield break;
        while (true)
        {
            yield return new WaitForSeconds(1 / frequence);
            someJob.Invoke();
        }
    }

    private void Start()
    {
        for (int i = 0; i < _frequences.Length; i++)
        {
            StartCoroutine(Job(_frequences[i], _events[i]));
        }
    }
}
