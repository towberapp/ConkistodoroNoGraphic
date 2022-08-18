using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowerImage : MonoBehaviour
{
    private TouchController _TouchController = null;

    private Image _Image = null;

    private void Start()
    {
        _TouchController = FindObjectOfType<TouchController>();
        _Image = GetComponent<Image>();
    }

    private void Update()
    {
        if (_TouchController.DraggingItem != null && _Image.color.a == 0)
        {
            Color newColor = _Image.color;
            newColor.a = 1;
            _Image.color = newColor;
            _Image.sprite = _TouchController.DraggingItem.Item.Sprite;
            _Image.SetNativeSize();
            GetComponent<RectTransform>().sizeDelta /= _TouchController.DraggingItem.Item.SpriteScaler;
        }
        else if (_TouchController.DraggingItem == null && _Image.color.a != 0)
        {
            Color newColor = _Image.color;
            newColor.a = 0;
            _Image.color = newColor;
        }
    }
}
