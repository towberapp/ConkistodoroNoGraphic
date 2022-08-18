using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug_18 : MonoBehaviour
{
    [SerializeField] private State _BugState;
    [SerializeField] private Animator _Light;

    public void Gone()
    {
        _BugState.Value = "Gone";
    }

    public void CloseLight() => _Light.Play("Closed");
}
