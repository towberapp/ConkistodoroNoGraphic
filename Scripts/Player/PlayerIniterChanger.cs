using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIniterChanger : MonoBehaviour
{
    [SerializeField]
    private int _Scene = 0;

    [SerializeField]
    private CharacterIniter _Initer = null;

    public void ChangeStart(PlayerStart newStart)
    {
        _Initer.ChangeStart(_Scene, newStart);
    }
}
