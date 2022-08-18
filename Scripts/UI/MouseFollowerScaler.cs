using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollowerScaler : MonoBehaviour
{
    [SerializeField] private MouseFollower _follower;
    [SerializeField] private float _scale;

    private void OnMouseEnter()
    {
        _follower.transform.localScale *= _scale;
    }

    private void OnMouseExit()
    {
        _follower.transform.localScale /= _scale;
        
    }
}
