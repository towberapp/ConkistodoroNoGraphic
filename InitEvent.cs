using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InitEvent : MonoBehaviour
{
    public UnityEvent onInit = new UnityEvent();


    private void OnEnable()
    {
        onInit.Invoke();
    }

}
