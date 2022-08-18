using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class MaterialChanger : MonoBehaviour
{
    [SerializeField]
    public float Brightness = 0;

    private Image _Image = null;

    private Material _Material;

    private float _PrevBrightness = 0;
    void Start()
    {
        _Image = GetComponent<Image>();
        _Material = new Material(_Image.material);
        _Material.name = gameObject.name;
        _Image.material = _Material;
    }

    void Update()
    {
        if (Brightness != _PrevBrightness)
        {
            _Image.material.SetFloat("_Brightness", Brightness);
            _Image.SetMaterialDirty();
            _PrevBrightness = Brightness;
        }
    }
}
