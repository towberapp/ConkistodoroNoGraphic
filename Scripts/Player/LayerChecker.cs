using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerChecker : MonoBehaviour
{
    private SpriteRenderer[] _SpriteRenderers = null;
    [SerializeField]
    private GameObject _AnimationsObject = null;

    [SerializeField]
    private int _StartMaxLayer = 0; 
    [SerializeField]
    private int _CurrentMaxLayer = 0; 

    [SerializeField]
    private List<int> _LayerHideOrder = new List<int>(); //Отслеживаем за каким объектом мы должны стоять

    private void Start()
    {
        _LayerHideOrder.Clear(); 
        _SpriteRenderers = _AnimationsObject.GetComponentsInChildren<SpriteRenderer>(true);
        _StartMaxLayer = FindMaxLayer();
        _CurrentMaxLayer = _StartMaxLayer;
    }
    private int FindMaxLayer()
    {
        int maxLayer = int.MinValue;
        for (int i = 0; i < _SpriteRenderers.Length; i++)
        {
            if (_SpriteRenderers[i].sortingOrder > maxLayer)
                maxLayer = _SpriteRenderers[i].sortingOrder;
        }
        return maxLayer;
    }
    public void SetMax()
    {
        HideInLayer(_StartMaxLayer);
    }
    public void HideObject(SpriteRenderer sr)
    {
        if (!sr)
            return;
        sr.sortingOrder -= (_CurrentMaxLayer - _StartMaxLayer);
    }
    public void HideInLayer(int layer)
    {
        for (int i = 0; i < _SpriteRenderers.Length; i++)
        {
            SpriteRenderer sr = _SpriteRenderers[i];
            sr.sortingOrder -= (_CurrentMaxLayer - layer);
        }
        _CurrentMaxLayer = layer;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.tag.Contains("Player") && collision.GetComponent<PlayerHider>() is PlayerHider hider)
        {
            _LayerHideOrder.Add(hider.Layer);
            _LayerHideOrder.Sort((a, b) =>
            {
                if (a < b)
                    return -1;
                else if (a > b)
                    return 1;
                return 0;
            });
            if (_LayerHideOrder[0] != _CurrentMaxLayer)
                HideInLayer(_LayerHideOrder[0]);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.tag.Contains("Player") && collision.GetComponent<PlayerHider>() is PlayerHider hider)
        {
            _LayerHideOrder.Remove(hider.Layer);
            if (_LayerHideOrder.Count == 0)
                HideInLayer(_StartMaxLayer);
            else if (_LayerHideOrder[0] != _CurrentMaxLayer)
                HideInLayer(_LayerHideOrder[0]);
        }
    }
}
