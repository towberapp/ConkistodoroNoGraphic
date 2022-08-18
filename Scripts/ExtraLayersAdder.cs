#if UNITY_EDITOR 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLayersAdder : MonoBehaviour
{
    [SerializeField]
    private int _ExtraLayer = 0;
    private List<SpriteRenderer> _Renderers = new List<SpriteRenderer>();
    // Start is called before the first frame update

    private void Start()
    {
        UpdateRenderes();
    }

    public void UpdateRenderes()
    {
        _Renderers.Clear();
        foreach (SpriteRenderer child in gameObject.GetComponentsInChildren<SpriteRenderer>(true))
            _Renderers.Add(child);
    }
    
    public void AddExtraLayers()
    {
        UpdateRenderes();
        _Renderers.ForEach((el) => el.sortingOrder += _ExtraLayer);
    }
    public void RemoveExtraLayers()
    {
        UpdateRenderes();
        _Renderers.ForEach((el) => el.sortingOrder -= _ExtraLayer);
    }

}
#endif