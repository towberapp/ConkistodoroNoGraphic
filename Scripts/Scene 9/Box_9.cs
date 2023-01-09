using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Box_9 : AnimatorObject
{
    private bool IsTubeOut = false,
                IsOn = false,
                Boom = false;

    [SerializeField]
    private SpriteRenderer _Bug = null;
    [SerializeField]
    private SpriteRenderer _Tube = null;
    [SerializeField]
    private AudioSource boomAudio;
 
    [SerializeField] private float fromRandomBom;
    [SerializeField] private float toRandomBom;

    [SerializeField] private UnityEvent listEvent;

    public void SetTubeOut()
    {
        animator.SetBool(nameof(IsTubeOut), true);
    }

    public void TurnOn()
    {
        animator.SetBool(nameof(IsOn), true);        
        Invoke("RandomBoom", Random.Range(fromRandomBom, toRandomBom));
    }

    private void RandomBoom()
    {
        listEvent.Invoke();
    }

    public void BoomBox()
    {
        animator.SetBool(nameof(Boom), true);
        boomAudio.Play();
    }

    public void SetTubeToBug()
    {
        _Tube.enabled = false;
    }

    public void SetBug()
    {
        _Bug.enabled = true;
    }
}
