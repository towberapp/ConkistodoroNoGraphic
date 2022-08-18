using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ColorFilter : MonoBehaviour
{
    private SpriteRenderer[] _Childs = null;

    public Color FilterColor = Color.white;

    private Color _PrevColor = Color.white;
    
    private void Start()
    {
        _Childs = GetComponentsInChildren<SpriteRenderer>(true);
    }

    public void ApplyFilter()
    {
        foreach (SpriteRenderer sr in _Childs)
            sr.color = FilterColor;
    }


    // Update is called once per frame
    void Update()
    {
        if (FilterColor != _PrevColor)
        {
            _PrevColor = FilterColor;
            ApplyFilter();
        }
    }
}
