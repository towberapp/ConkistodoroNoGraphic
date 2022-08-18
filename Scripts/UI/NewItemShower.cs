using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class NewItemShower : MonoBehaviour
{
    private Animator _Animator = null;
    private Image _Image = null;
    private RectTransform _RectTransform = null;

    private UIItem _NewItem = null;
    private void Start()
    {
        _Animator = GetComponent<Animator>();
        _Image = GetComponent<Image>();
        _RectTransform = GetComponent<RectTransform>();
    }
    public void Show(UIItem newItem)
    {
        IEnumerator Showing()
        {
            yield return null;
            Vector3 newPos = _RectTransform.position;
            newPos.x = newItem.GetComponent<RectTransform>().position.x;
            transform.position = newPos;
            _Image.sprite = newItem.Item.Sprite;
            _Image.SetNativeSize();
            _RectTransform.sizeDelta /= newItem.Item.SpriteScaler;
            _NewItem = newItem;
            _Animator.Play("Show");
        }
        StartCoroutine(Showing());
    }

    public void EnableItemAndDestroy()
    {
        _NewItem.GetComponent<Image>().enabled = true;
        Destroy(gameObject);
    }
}
