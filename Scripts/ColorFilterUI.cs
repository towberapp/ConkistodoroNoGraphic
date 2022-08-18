using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class ColorFilterUI : MonoBehaviour
{
    private Image[] _Childs = null;

    public Color FilterColor = Color.white;

    private Color _PrevColor = Color.white;

    private void Start()
    {
        _Childs = GetComponentsInChildren<Image>(true);
    }

    public void ApplyFilter()
    {
        foreach (var img in _Childs)
            img.color = FilterColor;
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
