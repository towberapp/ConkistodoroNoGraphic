using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class Point_19 : MonoBehaviour
{
    [SerializeField] private string _Name;
    [SerializeField] private Rope_19[] _Ropes;
    [SerializeField] private TimelineAsset[] _RopeClips;

    public string Name
    {
        get => _Name;
    }

    public Rope_19[] Ropes
    {
        get => _Ropes;
    }

    public TimelineAsset[] RopeClips
    {
        get => _RopeClips;
    }

    public void AttractToRope(int ropeId)
    {
        for (int i = 0; i < _Ropes.Length; i++)
            if (i == ropeId)
                _Ropes[i].IsAttractive = true;
            else
                _Ropes[i].IsAttractive = false;
    }

    public int GetRopeId(Rope_19 rope) => Array.FindIndex<Rope_19>(_Ropes, el => el == rope);

    public (Rope_19, TimelineAsset) GetRopeInfo(int ropeId)
    {
        return (_Ropes[ropeId], _RopeClips[ropeId]);
    }
}
