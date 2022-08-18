using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHider : MonoBehaviour
{
    [SerializeField]
    private int _Layer = 0;
    public int Layer
    {
        get
        {
            return _Layer;
        }
        private set
        {
            _Layer = value; 
        }
    }
}
