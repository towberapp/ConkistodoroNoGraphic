using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class UIItem : MonoBehaviour, IPointerDownHandler, IPointerClickHandler
{
    [SerializeField]
    private Item _Item = null;


    private Image _Image = null;

    [SerializeField]
    private TouchController _TouchController = null;

    [SerializeField]
    private ItemContainer _PlayerInventory;

    public Item Item
    {
        get
        {
            return _Item;
        }
    }

    public void Init(Item newItem, TouchController touchController)
    {
        _TouchController = touchController;
        _Item = newItem;
        _Image = GetComponent<Image>();
        _Image.sprite = _Item.Sprite;
        _Image.SetNativeSize();
        gameObject.name = _Item.Name;
        RectTransform trans = GetComponent<RectTransform>();
        trans.sizeDelta /= _Item.SpriteScaler;
        gameObject.SetActive(true);
    }

    public void Appear()
    {
        Color newColor = _Image.color;
        newColor.a = 1;
        _Image.color = newColor;
    }

    private void Hide()
    {
        Color newColor = _Image.color;
        newColor.a = 0;
        _Image.color = newColor;
        _TouchController.DraggingItem = this;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && _TouchController.enabled)
        {
            Hide();
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && _TouchController.enabled && Item.AnimationInfo != null)
        {
            FindObjectOfType<AnimationController>().SetItemAnim(Item.AnimationInfo);
        }
        if (eventData.button == PointerEventData.InputButton.Left && _TouchController.enabled && Item.OnClickEvent != null)
        {
            Item.OnClickEvent.Invoke();
        }
    }
}
