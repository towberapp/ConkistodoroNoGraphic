using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ClipParams", menuName = "ScriptableObjects/ClipParams", order = 52)]
public class ClipParams : ScriptableObject
{
    [SerializeField]
    private AudioClip _Clip = null;

    [SerializeField]
    [Range(0, 1)]
    private float _MaxVolume = 0.5f;

    public AudioClip Clip
    {
        get
        {
            return _Clip;
        }
    }

    public float MaxVolume
    {
        get
        {
            if (_MaxVolume > 1)
                return 1;
            else if (_MaxVolume < 0)
                return 0;
            else
                return _MaxVolume;
        }
    }
}
